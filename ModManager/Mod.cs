using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using MCM.MinecraftFramework;
using MCM.BackupFramework;

namespace ModManager
{
    public class Mod
    {
        public enum ModType
        {
            ZipMod,
            DirMod,
            JarMod
        }

        public enum ModLevel
        {
            mod,
            coremod
        }

        public Mod()
        { }

        public Mod(ModType type, ModLevel level, string name, string path, TinyMinecraftVersion version)
        {
            this.type = type;
            this.level = level;
            this.name = name;
            this.path = path;
            this.version = version;
        }

        public string name { get; set; }
        public ModType type { get; set; }
        public ModLevel level { get; set; }
        public string path { get; set; }
        public TinyMinecraftVersion version { get; set; }

        public static void InstallZipMod(Mod mod, Instance i)
        {
            string targetDir = GetTargetDir(mod, i);
            try
            {
                File.Copy(mod.path, targetDir);
            }
            catch (IOException e)
            {
                if (!e.Message.Contains("already exists"))
                    throw e;
            }
            Main.AddModToInstance(i, mod);
        }

        public static void DeleteMod(Mod mod, Instance i)
        {
            string targetDir = GetTargetDir(mod, i);
            if (mod.type == ModType.ZipMod)
                File.Delete(targetDir);
            else if (mod.type == ModType.DirMod)
                Directory.Delete(targetDir, true);
            else
                throw new Exception("Cannot delete jarmod");
            Main.GetModList(i).Remove(mod);
        }

        private static string GetTargetDir(Mod mod, Instance i)
        {
            string targetDir;
            if (mod.level == ModLevel.mod)
                targetDir = Main.GetModsPath(i);
            else if (mod.level == ModLevel.coremod)
                targetDir = Main.GetCoreModsPath(i);
            else
                throw new Exception("Dafuq? this isn't possible");

            targetDir = targetDir + "\\" + Path.GetFileName(mod.path);
            return targetDir;
        }

        public static void InstallMod(Mod mod, Instance i)
        {
            switch (mod.type)
            {
                case ModType.DirMod:
                    InstallDirMod(mod, i);
                    break;
                case ModType.JarMod:
                    InstallJarMod(mod, i);
                    break;
                case ModType.ZipMod:
                    InstallZipMod(mod, i);
                    break;
            }
        }

        public static void InstallDirMod(Mod mod, Instance i)
        {
            string targetDir = GetTargetDir(mod, i);
            CopyDir.Copy(mod.path, targetDir);
            Main.AddModToInstance(i, mod);
        }
        
        public static void InstallJarMod(Mod mod, Instance i)
        {
            string jarFile = i.MinecraftJarFilePath;
            FileStream stream = new FileStream(jarFile, FileMode.Open);
            ZipArchive arch = new ZipArchive(stream, ZipArchiveMode.Update);

            List<ZipArchiveEntry> markedRemove = new List<ZipArchiveEntry>();
            foreach (ZipArchiveEntry entry in arch.Entries)
            {
                if (entry.FullName.Contains("META-INF"))
                    markedRemove.Add(entry);
            }

            while (markedRemove.Count > 0)
            {
                markedRemove[0].Delete();
                markedRemove.Remove(markedRemove[0]);
            }

            foreach (string filePath in Directory.GetFiles(mod.path, "*", SearchOption.AllDirectories))
            {
                ZipArchiveEntry entry =  arch.CreateEntry(filePath.Replace(mod.path, ""));
                using (StreamWriter writer = new StreamWriter(entry.Open()))
                {
                    writer.Write(File.ReadAllBytes(filePath));
                }
            }
            Main.AddModToInstance(i, mod);
        }
    }
}
