using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day7
    {
        private List<int> total = new List<int>();

        public int Execute()
        {
            var lines = System.IO.File.ReadAllLines(@"../../../2022/Inputs/input7.txt");
            var directory = new Directory();
            var directorySize = 70000000;
            var headDir = directory;
            var activeDir = directory;
            var parentDir = directory;


            foreach (var line in lines)
            {
                if (line.Equals("$ ls"))
                {
                    continue;
                }

                else if (line.Contains(" cd "))
                {
                    var dir = line.Substring(line.IndexOf("cd") + 3);

                    switch (dir)
                    {
                        case "..":
                            activeDir = activeDir!.Parent;
                            parentDir = activeDir!.Parent;
                            break;
                        case "/":
                            activeDir = headDir;
                            parentDir = headDir;
                            break;
                        default:
                            if (activeDir!.Children.Any(x => x.Name == dir))
                            {
                                parentDir = activeDir;
                                activeDir = activeDir.Children.Find(x => x.Name == dir);
                            }
                            break;
                    }
                }

                else if (line.Contains("dir "))
                {
                    var dir = line.Substring(line.IndexOf("dir") + 4);

                    var newChild = new Directory(dir, activeDir!);
                    activeDir!.Children.Add(newChild);
                }

                else
                {
                    var size = int.Parse(line.Substring(0, line.IndexOf(" ")));
                    var fileName = line.Substring(line.IndexOf(" "));
                    activeDir.Files.Add(new Files() { Name = fileName, Size = size});
                }
            }

            var count = 0;
            InOrder(headDir, 0);
            directorySize -= headDir.DirectorySize;
            directorySize = 30000000 - directorySize;

            GetDirectorySizes(headDir, directorySize);
            total.Sort();
            return total[0];
        }

        private int InOrder(Directory node, int count)
        {
            if (node == null)
            {
                return count;
            }

            count = 0;
            node.Children.ForEach(x =>
            {
                count = InOrder(x, count);
            });

            count = 0;
            foreach (var item in node.Files)
            {
                 count += item.Size;
            }

            foreach (var item in node.Children)
            {
                count += item.DirectorySize;
            }

            node.DirectorySize = count;
            return count;
        }

        private void GetDirectorySizes(Directory node, int directorySize)
        {
            if (node == null)
            {
                return;
            }

            if (node.DirectorySize >= directorySize)
            {
                total.Add(node.DirectorySize);
            }


            node.Children.ForEach(x =>
            {
                GetDirectorySizes(x, directorySize);
            });

            return;
        }
    }

    public class Directory
    {
        public Directory()
        {
            Name = "/";
            Files = new List<Files>();
            Parent = null;
            Children = new List<Directory>();
            DirectorySize = 0;
        }

        public Directory(string name, Directory parent)
        {
            Name = name;
            Files = new List<Files>();
            Parent = parent;
            Children = new List<Directory>();
            DirectorySize = 0;
        }

        public string Name { get; set; }
        public List<Files> Files { get; set; }
        public List<Directory> Children { get; set; }
        public Directory? Parent { get; set; }
        public int DirectorySize { get; set; }
    }

    public class Files
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }
}