using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    class RemainingHomophoneListExporter
    {
        #region Public Methods
        public void Export(string fileName, IEnumerable<HomophoneGroup> homophoneGroupList)
        {
            IEnumerable<HomophoneGroup> sortedList = from homophoneGroup in homophoneGroupList orderby homophoneGroup.Count descending select homophoneGroup;

            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                foreach (HomophoneGroup homophoneGroup in sortedList)
                {
                    if (!IsDesiredHomophoneGroup(homophoneGroup))
                    {
                        if (homophoneGroup.Count > 1)
                        {
                            foreach (string name in homophoneGroup)
                            {
                                streamWriter.WriteLine(name);
                            }
                            streamWriter.WriteLine("");
                        }
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        private bool IsDesiredHomophoneGroup(HomophoneGroup homophoneGroup)
        {
            return IsOrEr(homophoneGroup) ||
                IsIseIze(homophoneGroup) ||
                IsIseIzed(homophoneGroup) ||
                IsSC(homophoneGroup) ||
                IsNsNes(homophoneGroup) ||
                IsMsMes(homophoneGroup) ||
                IsLsLes(homophoneGroup) ||
                IsKsKes(homophoneGroup) ||
                IsChsChes(homophoneGroup) ||
                IsRsRes(homophoneGroup) ||
                IsDsDes(homophoneGroup) ||
                IsTsTes(homophoneGroup) ||
                IsSingZing(homophoneGroup) ||
                IsOsOes(homophoneGroup);
        }

        private bool IsSingZing(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 4 && wordList[1].Length > 4)
                {
                    if (wordList[0].EndsWith("zing") && wordList[1].EndsWith("sing"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("sing") && wordList[1].EndsWith("zing"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsChsChes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("ches") && wordList[1].EndsWith("chs"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("chs") && wordList[1].EndsWith("ches"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsRsRes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("res") && wordList[1].EndsWith("rs"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("rs") && wordList[1].EndsWith("res"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsTsTes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("tes") && wordList[1].EndsWith("ts"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("ts") && wordList[1].EndsWith("tes"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsDsDes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("des") && wordList[1].EndsWith("ds"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("ds") && wordList[1].EndsWith("des"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsKsKes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("kes") && wordList[1].EndsWith("ks"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("ks") && wordList[1].EndsWith("kes"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsLsLes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("les") && wordList[1].EndsWith("ls"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("ls") && wordList[1].EndsWith("les"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsOsOes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("oes") && wordList[1].EndsWith("os"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("os") && wordList[1].EndsWith("oes"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsNsNes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("nes") && wordList[1].EndsWith("ns"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("ns") && wordList[1].EndsWith("nes"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsMsMes(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length > 3)
                {
                    if (wordList[0].EndsWith("mes") && wordList[1].EndsWith("ms"))
                    {
                        return true;
                    }
                    else if (wordList[0].EndsWith("ms") && wordList[1].EndsWith("mes"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsOrEr(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 2 && wordList[1].Length == wordList[0].Length)
                {
                    if (wordList[0].Substring(0, wordList[0].Length - 2) == wordList[1].Substring(0, wordList[1].Length - 2))
                    {
                        if (wordList[0].EndsWith("er") && wordList[1].EndsWith("or"))
                        {
                            return true;
                        }
                        else if (wordList[0].EndsWith("or") && wordList[1].EndsWith("er"))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsIseIze(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 3 && wordList[1].Length == wordList[0].Length)
                {
                    if (wordList[0].Substring(0, wordList[0].Length - 3) == wordList[1].Substring(0, wordList[1].Length - 3))
                    {
                        if (wordList[0].EndsWith("ise") && wordList[1].EndsWith("ize"))
                        {
                            return true;
                        }
                        else if (wordList[0].EndsWith("ize") && wordList[1].EndsWith("ise"))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsIseIzed(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 4 && wordList[1].Length == wordList[0].Length)
                {
                    if (wordList[0].Substring(0, wordList[0].Length - 4) == wordList[1].Substring(0, wordList[1].Length - 4))
                    {
                        if (wordList[0].EndsWith("ised") && wordList[1].EndsWith("ized"))
                        {
                            return true;
                        }
                        else if (wordList[0].EndsWith("ized") && wordList[1].EndsWith("ised"))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsSC(HomophoneGroup homophoneGroup)
        {
            List<string> wordList = new List<string>(homophoneGroup);

            if (wordList.Count == 2)
            {
                if (wordList[0].Length > 1 && wordList[1].Length == wordList[0].Length)
                {
                    if (wordList[0].Substring(1) == wordList[1].Substring(1))
                    {
                        if (wordList[0].StartsWith("s") && wordList[1].StartsWith("c"))
                        {
                            return true;
                        }
                        else if (wordList[0].StartsWith("c") && wordList[1].StartsWith("s"))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        #endregion
    }
}
