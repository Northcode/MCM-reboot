using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.BackupFramework
{
    /// <summary>
    /// A minecraft version
    /// 
    /// Contains info on the version of minecraft,
    /// mods used in the version and snapshot information
    /// 
    /// An MCVersion is not just a version of minecraft, but also a custom version, containing mods etc...
    /// </summary>
    public class MCVersion
    {
        public string Name { get; set; }

        //Well.. Duh...
        public bool IsSnapshot { get; set; }

        //Minecraft versions come in Major.Minor.Revision
        //Technically Major should be 0 for beta! This is for comparing beta versions with 1.0+ versions!
        public int Major { get; set; }
        public int Minor  { get; set; }
        public int Revision { get; set; }

        //Versions for Snapshots
        public int SnapshotYear { get; set; }
        public int SnapshotWeek { get; set; }
        public int SnapshotWeekVer { get; set; } //0: a , 1: b, 2: c ...

        //Making it do stuff

        public static bool operator <(MCVersion a,MCVersion b)
        {
            if (a.IsSnapshot && b.IsSnapshot)
            {
                return ((a.SnapshotYear < b.SnapshotYear) ||
                    (a.SnapshotYear == b.SnapshotYear && a.SnapshotWeek < a.SnapshotWeek) ||
                    (a.SnapshotYear == b.SnapshotYear && a.SnapshotWeek == b.SnapshotWeek && a.SnapshotWeekVer < b.SnapshotWeekVer));
            }
            else
            {
                return ((a.Major < b.Major) ||
                    (a.Major == b.Major && a.Minor < b.Minor) ||
                    (a.Major == b.Major && a.Minor == b.Minor && a.Revision < b.Revision));
            }
        }

        /// <summary>
        /// Takes modded info into account
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator==(MCVersion a, MCVersion b)
        {
            if (a.IsSnapshot && b.IsSnapshot)
            {
                return (a.SnapshotYear == b.SnapshotYear && a.SnapshotWeek == b.SnapshotWeek && a.SnapshotWeekVer == b.SnapshotWeekVer);
            }
            else
            {
                return (a.Major == b.Major && a.Minor == b.Minor && a.Revision == b.Revision);
            }
        }

        /// <summary>
        /// Takes modded info into account
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(MCVersion a, MCVersion b)
        {
            if (a.IsSnapshot && b.IsSnapshot)
            {
                return (a.SnapshotYear != b.SnapshotYear || a.SnapshotWeek != b.SnapshotWeek || a.SnapshotWeekVer != b.SnapshotWeekVer);
            }
            else
            {
                return (a.Major != b.Major || a.Minor != b.Minor || a.Revision != b.Revision);
            }
        }

        public static bool operator >(MCVersion a, MCVersion b)
        {
            if (a.IsSnapshot)
            {
                return ((a.SnapshotYear > b.SnapshotYear) ||
                    (a.SnapshotYear == b.SnapshotYear && a.SnapshotWeek > a.SnapshotWeek) ||
                    (a.SnapshotYear == b.SnapshotYear && a.SnapshotWeek == b.SnapshotWeek && a.SnapshotWeekVer > b.SnapshotWeekVer));
            }
            else
            {
                return ((a.Major > b.Major) ||
                    (a.Major == b.Major && a.Minor > b.Minor) ||
                    (a.Major == b.Major && a.Minor == b.Minor && a.Revision > b.Revision));
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ IsSnapshot.GetHashCode() ^ Major.GetHashCode() ^ Minor.GetHashCode() ^ Revision.GetHashCode() ^
                SnapshotYear.GetHashCode() ^ SnapshotWeek.GetHashCode() ^ SnapshotWeekVer.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            if (IsSnapshot)
            {
                return (SnapshotYear - 2000) + "w" + SnapshotWeek + (char)(SnapshotWeekVer + 97);
            }
            else
            {
                return Major + "." + Minor + "." + Revision;
            }
        }
    }
}
