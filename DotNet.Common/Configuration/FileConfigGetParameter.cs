using System;
using System.IO;
using System.Text;

using DotNet.Common.Diagnostics;
using DotNet.Common.IO;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 获取文件配置参数。
    /// </summary>
    public abstract class FileConfigGetParameter : IConfigParameter
    {
        private string[] _files;
        private string _directory;
        private string _filter;
        private bool _includeSubdirectories;

        /// <summary>
        /// 初始化 <see cref="FileConfigGetParameter"/> 类的新实例。
        /// </summary>
        public FileConfigGetParameter()
        {
            this.Path = string.Empty;
            this.Filter = "*.config";
            this.IncludeSubdirectories = false;
        }

        /// <summary>
        /// 给定配置文件所在的目录，初始化 <see cref="FileConfigGetParameter"/> 类的新实例。
        /// </summary>
        /// <param name="path">配置文件的路径。</param>
        public FileConfigGetParameter(string path)
            : this(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileName(path))
        {
        }

        /// <summary>
        /// 给定配置文件所在的目录，初始化 <see cref="FileConfigGetParameter"/> 类的新实例。
        /// </summary>
        /// <param name="path">配置文件所在的目录。</param>
        /// <param name="filter">配置文件的类型。例如，“*.config”所有以.config扩展名的配置文件。</param>
        public FileConfigGetParameter(string path, string filter)
            : this(path, filter, false)
        {
        }

        /// <summary>
        /// 给定配置文件所在的目录和文件类型，初始化 <see cref="FileConfigGetParameter"/> 类的新实例。
        /// </summary>
        /// <param name="path">配置文件的路径。</param>
        /// <param name="includeSubdirectories">指示在所有子目录中搜索配置文件的标识。</param>
        public FileConfigGetParameter(string path, bool includeSubdirectories)
            : this(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileName(path), includeSubdirectories)
        {
        }

        /// <summary>
        /// 给定配置文件所在的目录和文件类型，初始化 <see cref="FileConfigGetParameter"/> 类的新实例。
        /// </summary>
        /// <param name="path">配置文件所在的目录。</param>
        /// <param name="filter">配置文件的类型。例如，“*.config”所有以.config扩展名的配置文件。</param>
        /// <param name="includeSubdirectories">指示在所有子目录中搜索配置文件的标识。</param>
        public FileConfigGetParameter(string path, string filter, bool includeSubdirectories)
        {
            Check.Argument.IsNotEmpty("path", path);
            Check.Argument.IsNotEmpty("filter", filter);

            this.Path = PathUtils.GetFullPath(path);
            this.Filter = filter;
            this.IncludeSubdirectories = includeSubdirectories;

            //this.RefreshFiles();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasFiles
        {
            get
            {
                return this.RefreshFiles();
            }
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        public string[] Files
        {
            get 
            {
                if (_files == null || _files.Length == 0)
                {
                    this.RefreshFiles();
                }

                return _files ?? new string[]{}; 
            }
        }

        /// <summary>
        /// 获取或设置配置文件目录的路径。
        /// </summary>
        /// <value>配置文件的路径。默认值为空字符串 ("")。</value>
        /// <remarks>
        /// 这是某个目录的完全限定路径。如果 IncludeSubdirectories 属性为 true，则此目录为配置文件所有的根目录；否则此目录为配置文件所在的唯一目录。
        /// 若要从某个特定配置文件中获取配置信息，请将 <see cref="Path"/> 属性设置为完全限定的正确目录，并将 <see cref="Filter"/> 属性设置为配置文件名。
        /// </remarks>
        public string Path
        {
            get { return _directory; }
            set
            {
                value = value ?? string.Empty;
                if (string.Compare(_directory, value, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    _directory = value.Replace("\\", "/");
                    if (!_directory.EndsWith("/"))
                    {
                        _directory = _directory + @"/";
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置筛选字符串，用于确定在目录中那些文件用于配置。
        /// </summary>
        /// <value>筛选器字符串。默认值为“*.*”（所有文件用于配置）。</value>
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = "*.config";
                }

                value = value.Replace("\\", @"/");
                if (value.StartsWith(@"/"))
                {
                    value = value.Substring(1);
                }

                if (string.Compare(_filter, value, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    _filter = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否要从指定路径中的子目录读取配置文件。
        /// </summary>
        /// <value>如果要从子目录中读取配置文件，则为 <b>true</b>；否则为 <b>false</b>。默认为 <b>false</b>。</value>
        public bool IncludeSubdirectories
        {
            get { return _includeSubdirectories; }
            set
            {
                if (_includeSubdirectories != value)
                {
                    _includeSubdirectories = value;
                }
            }
        }

        /// <summary>
        /// 获取配置类型的名称。
        /// </summary>
        public virtual string Name
        {
            get
            {
                return string.Format("FileConfig_{0}_{1}", Path, Filter);
            }
        }

        /// <summary>
        /// 获取配置类型的名称。
        /// </summary>
        public virtual string GroupName
        {
            get
            {
                return string.Format("FileConfig_{0}_{1}", Path, Filter);
            }
        }

        /// <summary>
        /// 刷新配置入口参数对应的配置文件。
        /// </summary>
        public bool RefreshFiles()
        {
            if (Directory.Exists(this.Path))
            {
                SearchOption searchOption = this.IncludeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                _files = Directory.GetFiles(this.Path, this.Filter, searchOption);
            }

            return _files != null && _files.Length > 0;
        }

        /// <summary>
        /// 配置文件路径。
        /// </summary>
        public string FilePaths
        {
            get
            {              
                StringBuilder filePaths = new StringBuilder();
                foreach (string path in Files)
                {
                    filePaths.Append(path);
                }
                return filePaths.ToString();
            }
        }
    }
}