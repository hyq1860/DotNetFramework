using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNet.EnterpriseWebSite.Ajax
{
    using System.Data;

    using DotNet.Base;
    using DotNet.Base.Contract;
    using DotNet.Base.Service;
    using DotNet.Common;
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.Contract.IService;
    using DotNet.ContentManagement.Service;
    using DotNet.Data;
    using DotNet.EnterpriseWebSite.ViewModel;
    using DotNet.Web.AjaxHandler;
    using DotNet.Web.Configuration;

    /// <summary>
    /// Summary description for Common
    /// </summary>
    public class Common : WebHandler
    {

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "演示返回动态对象和服务端缓存", HttpMethod = "get")]
        public object GetVar2()
        {

            IModuleService moduleService = new ModuleService();
            List<Module> menus = moduleService.GetTreeModules();

            // 每页显示记录数
            int pageSize = 10;
            // 记录总数
            int rowCount = menus.Count;
            // 总页数
            int pageCount = rowCount % pageSize == 0 ? rowCount / pageSize : rowCount / pageSize + 1;

            var resultObj = new JQGridDataResult
            {
                // 总页数
                PageCount = pageCount,
                // 当前页
                PageIndex = 1,
                // 总记录数
                Total = rowCount,
                // 数据
                Data = menus
            };
            return resultObj;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "演示返回动态对象和服务端缓存", HttpMethod = "get")]
        public object GetProductCategory()
        {
            IProductCategoryService productCategoryService=new ProductCategoryService();

            var data = productCategoryService.GetProductCategory();

            // 每页显示记录数
            int pageSize = 10;
            // 记录总数
            int rowCount = data.Count;
            // 总页数
            int pageCount = rowCount % pageSize == 0 ? rowCount / pageSize : rowCount / pageSize + 1;

            var resultObj = new JQGridDataResult
            {
                // 总页数
                PageCount = pageCount,
                // 当前页
                PageIndex = 1,
                // 总记录数
                Total = rowCount,
                // 数据
                Data =from item in data
                      select new 
                          {
                              cell=new string[]
                                  {
                                     item.Id.ToString(),
                                     item.Name,
                                     item.Desc,
                                     item.CategoryId.ToString(),
                                     item.MediumPicture,
                                     
                                     item.Depth.ToString(),
                                     item.Parent,
                                     (item.Rgt==item.Lft+1).ToString(),
                                     "true",
                                     "true"
                                  }
                          }
            };
            return resultObj;

        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "演示返回动态对象和服务端缓存", HttpMethod = "get")]
        public object GetAllProductCategory()
        {
            IProductCategoryService productCategoryService = new ProductCategoryService();

            return productCategoryService.GetProductCategory().Where(s=>s.Depth!=0).ToList();
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "获取栏目分类", HttpMethod = "get")]
        public List<ArticleCategoryInfo> GetAllArticleCategory()
        {
            IArticleCategoryService articleCategoryService=new ArticleCategoryService();
            var data= articleCategoryService.GetAllArticleCategory().Where(s => s.Depth != 0).ToList();
            var group=ConfigHelper.ListConfig.GetListItems("ArticleCategoryType");
            data.ForEach((s) =>
                { s.TypeName = group.FirstOrDefault(item => item.Value == s.CategoryType.ToString()).Text; });
            return data;
        }
        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "新增产品分类", HttpMethod = "get")]
        public bool InsertProductCategory()
        {
            //var option = int.Parse(HttpContext.Current.Request.Form["parentSelect"]);
            var model = new ProductCategoryInfo();
            model.Name=HttpContext.Current.Request.Form["CategoryName"];
            model.Desc = HttpContext.Current.Request.Form["desc"];
            model.MediumPicture = HttpContext.Current.Request.Form["mediumpicture"];
            model.ParentId = int.Parse(HttpContext.Current.Request.Form["parentSelect"]);
            var display = 0;
            if(int.TryParse(HttpContext.Current.Request.Form["DisplayOrder"],out display))
            {
                model.DisplayOrder = display;
            }
            else
            {
                model.DisplayOrder = display;
            }
            IProductCategoryService productCategoryService=new ProductCategoryService();
            return productCategoryService.Insert(model, 1) > 0;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "删除分类，删除子结点", HttpMethod = "get")]
        public object DeleteProductCategoryById(int id)
        {
            IProductCategoryService productCategoryService=new ProductCategoryService();

            var productCategory = productCategoryService.GetProductCategoryById(id);
            if(productCategory!=null)
            {
                //WHERE Lft BETWEEN @MyLeft AND @MyRight;
                var all =
                    productCategoryService.GetProductCategory().Where(
                        s => s.Lft >= productCategory.Lft && s.Rgt <= productCategory.Rgt);
                IProductService productService=new ProductService();
                foreach (ProductCategoryInfo productCategoryInfo in all)
                {
                    productService.DeleteProductByCategoryId(productCategory.Id);
                }
            }

            return productCategoryService.DeleteById(id);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "删除分类,不删除子结点", HttpMethod = "get")]
        public object DeleteProductCategoryOnlyById(int id)
        {
            IProductCategoryService productCategoryService = new ProductCategoryService();
            return productCategoryService.DeleteCurrentNodeOnlyById(id);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "更新分类", HttpMethod = "get")]
        public object UpdateProductCategoryByCategoryId()
        {
            var model = new ProductCategoryInfo();
            model.Id =  Convert.ToInt32(HttpContext.Current.Request.Params["CategoryId"]);
			model.Name = HttpContext.Current.Request.Params["CategoryName"];
			model.Desc = (string)HttpContext.Current.Request.Params["desc"];
			model.BigPicture = HttpContext.Current.Request.Params["BigPicture"];
			model.SmallPicture = HttpContext.Current.Request.Params["SmallPicture"];
            model.MediumPicture = HttpContext.Current.Request.Params["mediumpicture"];
            model.Content = HttpContext.Current.Request.Params["desc"];
            var display = 0;
            if (int.TryParse(HttpContext.Current.Request.Form["DisplayOrder"], out display))
            {
                model.DisplayOrder = display;
            }
            else
            {
                model.DisplayOrder = display;
            }
            //model.ParentId = int.Parse(HttpContext.Current.Request.Form["parentSelect"]);
            IProductCategoryService productCategoryService = new ProductCategoryService();
            return productCategoryService.Update(model);
        }



        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "新增产品", HttpMethod = "POST")]
        public object InsertProduct()
        {
            var model = new ProductInfo();
            //model.Id = Convert.ToInt32(HttpContext.Current.Request.Params["id"]);
            model.Title = (string)HttpContext.Current.Request.Params["title"];
            model.CategoryId = Convert.ToInt32(HttpContext.Current.Request.Params["productCategory"]);
            //model.Price = Convert.ToDouble(HttpContext.Current.Request.Params["price"]);
            model.Content = (string)HttpContext.Current.Request.Params["Content"];
            model.Desc = (string)HttpContext.Current.Request.Params["Desc"];//产品简介
            //model.IsOnline = HttpContext.Current.Request.Params["isonline"];
            model.BigPicture = HttpContext.Current.Request.Params["bigpicture"];
            model.SmallPicture = HttpContext.Current.Request.Params["smallpicture"];
            model.MediumPicture = (string)HttpContext.Current.Request.Params["mediumpicture"];
            model.Specifications = (string)HttpContext.Current.Request.Params["specifications"];
            model.AfterService = (string)HttpContext.Current.Request.Params["afterservice"];
            model.InDate = DateTime.Now;
            model.FileUrl = HttpContext.Current.Request.Params["filedown"];
            model.Keywords = HttpContext.Current.Request.Params["Keywords"];
            model.MetaDesc = HttpContext.Current.Request.Params["Description"];
            model.DisplayOrder = OrderGenerator.NewOrder();
            IProductService productService = new ProductService();
            return productService.Add(model) > 0;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "演示返回动态对象和服务端缓存", HttpMethod = "post")]
        public object UpdateProduct()
        {
            var model = new ProductInfo();
            model.Id = Convert.ToInt32(HttpContext.Current.Request.Params["ProductId"]);
            model.Title = (string)HttpContext.Current.Request.Params["title"];
            model.CategoryId = Convert.ToInt32(HttpContext.Current.Request.Params["productCategory"]);
            //model.Price = Convert.ToDouble(HttpContext.Current.Request.Params["price"]);
            model.Content = (string)HttpContext.Current.Request.Params["Content"];
            model.Desc = (string)HttpContext.Current.Request.Params["Desc"];//产品简介
            //model.IsOnline = HttpContext.Current.Request.Params["isonline"];
            model.BigPicture = HttpContext.Current.Request.Params["bigpicture"];
            model.SmallPicture = HttpContext.Current.Request.Params["smallpicture"];
            model.MediumPicture = (string)HttpContext.Current.Request.Params["mediumpicture"];
            model.Specifications = (string)HttpContext.Current.Request.Params["specifications"];
            model.AfterService = (string)HttpContext.Current.Request.Params["afterservice"];
            model.InDate = DateTime.Now;
            model.FileUrl = HttpContext.Current.Request.Params["filedown"];
            model.Keywords = HttpContext.Current.Request.Params["Keywords"];
            model.MetaDesc = HttpContext.Current.Request.Params["Description"];
            IProductService productService = new ProductService();
            return productService.Update(model) > 0;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "删除商品", HttpMethod = "post")]
         public int DeleteProductById(int id)
         {
             IProductService productService = new ProductService();
             return productService.DeleteById(id);
         }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "获取分页数据", HttpMethod = "get")]
        public object GetProducts()
        {

            // 每页显示记录数
            int pageSize = int.Parse(HttpContext.Current.Request.QueryString["rows"]);
            // 当前页
            int pageIndex = int.Parse(HttpContext.Current.Request.QueryString["page"]);

            IProductService productService = new ProductService();
            List<ProductInfo> data = productService.GetProducts(pageSize, pageIndex, string.Empty);

           
            // 记录总数
            int rowCount = productService.GetProductCount(string.Empty);
            // 总页数
            int pageCount = rowCount % pageSize == 0 ? rowCount / pageSize : rowCount / pageSize + 1;

            var resultObj = new JQGridDataResult
            {
                // 总页数
                PageCount = pageCount,
                // 当前页
                PageIndex = pageIndex,
                // 总记录数
                Total = rowCount,
                // 数据
                Data = data
            };
            return resultObj;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "获取基本内容数据", HttpMethod = "get")]
        public object GetBasicContents()
        {
            IBasicContentService basicContentService=new BasicContentService();

            var data=basicContentService.GetBasicContents();
            // 每页显示记录数
            int pageSize = 10;
            // 记录总数
            int rowCount = data.Count;
            // 总页数
            int pageCount = rowCount % pageSize == 0 ? rowCount / pageSize : rowCount / pageSize + 1;

            var resultObj = new JQGridDataResult
            {
                // 总页数
                PageCount = pageCount,
                // 当前页
                PageIndex = 1,
                // 总记录数
                Total = rowCount,
                // 数据
                Data = data
            };
            return resultObj;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "更新基本内容数据", HttpMethod = "post")]
        public int UpdateBasicContent()
        {
            var model = new BasicContentInfo();
            model.Id = Convert.ToInt32(HttpContext.Current.Request.Params["BasicContentId"]);
            model.Title = HttpContext.Current.Request.Params["Title"];
            model.Category = HttpContext.Current.Request.Params["Category"];
            model.LinkPath = HttpContext.Current.Request.Params["LinkPath"];
            model.Content = HttpContext.Current.Request.Params["Content"];
            model.Summary = HttpContext.Current.Request.Params["Summary"];
            IBasicContentService basicContentService=new BasicContentService();
            return basicContentService.Update(model);
        }


        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "获取分类数据", HttpMethod = "post")]
        public List<ComboTree> GetComboTreeCategory(int id)
        {
            IProductCategoryService productCategoryService = new ProductCategoryService();
            var data = productCategoryService.GetProductCategory();
            List<ComboTree> result=new List<ComboTree>();

            var root = data.Where(s => s.Depth == id);
            foreach (var categoryInfo in root)
            {
                var comboTree = new ComboTree();
                comboTree.Id = categoryInfo.Id;
                comboTree.Text = categoryInfo.Name;
                comboTree.Children = GetComboTreeChildren(comboTree, categoryInfo, data);
                result.Add(comboTree);
            }
            return result;
        }

        public List<ComboTree> GetComboTreeChildren(ComboTree comboTree,ProductCategoryInfo category,List<ProductCategoryInfo> data)
        {
            var childrens =
                data.Where(s => s.Depth == category.Depth + 1 && s.Lft > category.Lft && s.Rgt < category.Rgt);
            var childs = new List<ComboTree>();
            foreach (var productCategoryInfo in childrens)
            {
                var child = new ComboTree();
                child.Id = productCategoryInfo.Id;
                child.Text = productCategoryInfo.Name;
                childs.Add(child);
            }

            comboTree.Children = childs;

            foreach (var child in comboTree.Children)
            {
                var temp = data.FirstOrDefault(s => s.Id == child.Id);
                if(temp!=null)
                {
                    this.GetComboTreeChildren(child, temp, data);
                }
            }
            return comboTree.Children;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "获取文章分类数据", HttpMethod = "post")]
        public List<ComboTree> GetComboTreeArticleCategory(int id)
        {
            IArticleCategoryService articleCategoryService = new ArticleCategoryService();
            var data = articleCategoryService.GetAllArticleCategory();
            List<ComboTree> result = new List<ComboTree>();

            var root = data.Where(s => s.Depth == id);
            foreach (var articleCategoryInfo in root)
            {
                var comboTree = new ComboTree();
                comboTree.Id = articleCategoryInfo.Id;
                comboTree.Text = articleCategoryInfo.Name;
                comboTree.Children = GetComboTreeChildren2(comboTree, articleCategoryInfo, data);
                result.Add(comboTree);
            }
            return result;
        }

        public List<ComboTree> GetComboTreeChildren2(ComboTree comboTree, ArticleCategoryInfo category, List<ArticleCategoryInfo> data)
        {
            var childrens =
                data.Where(s => s.Depth == category.Depth + 1 && s.Lft > category.Lft && s.Rgt < category.Rgt);
            var childs = new List<ComboTree>();
            foreach (var productCategoryInfo in childrens)
            {
                var child = new ComboTree();
                child.Id = productCategoryInfo.Id;
                child.Text = productCategoryInfo.Name;
                childs.Add(child);
            }

            comboTree.Children = childs;

            foreach (var child in comboTree.Children)
            {
                var temp = data.FirstOrDefault(s => s.Id == child.Id);
                if (temp != null)
                {
                    this.GetComboTreeChildren2(child, temp, data);
                }
            }
            return comboTree.Children;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "获取网站配置", HttpMethod = "post")]
        public object GetSiteConfig()
        {
            ISiteOptionService siteOptionService = new SiteOptionService();

            var data = siteOptionService.GetSiteOption();

            // 每页显示记录数
            int pageSize = 10;
            // 记录总数
            int rowCount = data.Count;
            // 总页数
            int pageCount = rowCount % pageSize == 0 ? rowCount / pageSize : rowCount / pageSize + 1;

            var resultObj = new JQGridDataResult
            {
                // 总页数
                PageCount = pageCount,
                // 当前页
                PageIndex = 1,
                // 总记录数
                Total = rowCount,
                // 数据
                Data = data
            };
            return resultObj;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "更新网站配置", HttpMethod = "post")]
        public int UpdateSiteById(string OptionId, string OptionValue)
        {
             ISiteOptionService siteOptionService = new SiteOptionService();
            var model = new SiteOptionInfo();
            model.OptionId = int.Parse(OptionId);
            model.OptionValue = OptionValue;
            SiteConfigManager.IsModify = true;
            return siteOptionService.Update(model);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "更新int", HttpMethod = "post")]
        public int CommonUpdateInt(string tableName, string columnName, int value, string keyColumn, int primaryKey)
        {
            ICommonService commonService=new CommonService();
            return commonService.UpdateIntColumn(tableName, columnName, value, keyColumn, primaryKey);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "上移", HttpMethod = "post")]
        public int CommonUp(string tableName, string primaryColumn, Int64 primaryValue, string orderColumn,long orderNumber)
        {
            ICommonService commonService = new CommonService();
            return commonService.Up(tableName, primaryColumn, primaryValue, orderColumn,orderNumber);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "下移", HttpMethod = "post")]
        public int CommonDown(string tableName, string primaryColumn, Int64 primaryValue, string orderColumn, long orderNumber)
        {
            ICommonService commonService = new CommonService();
            return commonService.Down(tableName, primaryColumn, primaryValue, orderColumn,orderNumber);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "下移", HttpMethod = "post")]
        public int MoveDown(string tableName, string keyColumn, string orderColumn, long p1, long p2, long o1, long o2)
        {
            ICommonService commonService = new CommonService();
            return commonService.MoveDown(tableName, keyColumn, orderColumn, p1, p2, o1, o2);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "上移", HttpMethod = "post")]
        public int MoveUp(string tableName, string keyColumn, string orderColumn, long p1, long p2, long o1, long o2)
        {
            ICommonService commonService = new CommonService();
            return commonService.MoveUp(tableName, keyColumn, orderColumn, p1, p2, o1, o2);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "新增栏目", HttpMethod = "post")]
        public int InsertArticleCategory()
        {
            IArticleCategoryService articleCategoryService=new ArticleCategoryService();
            var model = new ArticleCategoryInfo();
            model.Title = HttpContext.Current.Request.Params["ArticleCategoryTitle"];
            model.Name = HttpContext.Current.Request.Params["ArticleCategoryTitle"];
            model.UrlPath = HttpContext.Current.Request.Params["UrlPath"];
            model.Keywords = HttpContext.Current.Request.Params["ArticleCategoryKeywords"];
            model.MetaDesc = HttpContext.Current.Request.Params["ArticleCategoryMetaDesc"];
            model.CategoryType = Convert.ToInt32(HttpContext.Current.Request.Params["ArticleCategoryType"]??"0");
            var parentId = HttpContext.Current.Request.Params["ParentArticleCategory"] ?? "0";
            model.ParentId = Convert.ToInt32(parentId);
            model.Description = HttpContext.Current.Request.Params["ArticleCategoryDesc"];
            model.InUserId = 0;
            model.InDate = DateTime.Now;
            model.EditDate = DateTime.Now;
            model.EditUserId = 0;
            model.DisplayOrder = OrderGenerator.NewOrder();
            model.DataStatus = 1;
            return articleCategoryService.Insert(model);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "更新栏目", HttpMethod = "post")]
        public int UpdateArticleCategoryByCategoryId()
        {
            IArticleCategoryService articleCategoryService = new ArticleCategoryService();
            var model = new ArticleCategoryInfo();
            model.Id = Convert.ToInt32(HttpContext.Current.Request.Params["ArticleCategoryId"]);
            model.Title = HttpContext.Current.Request.Params["ArticleCategoryTitle"];
            model.Name = HttpContext.Current.Request.Params["ArticleCategoryTitle"];
            model.UrlPath = HttpContext.Current.Request.Params["UrlPath"];
            model.Keywords = HttpContext.Current.Request.Params["ArticleCategoryKeywords"];
            model.MetaDesc = HttpContext.Current.Request.Params["ArticleCategoryMetaDesc"];
            //var parentId = HttpContext.Current.Request.Params["ParentArticleCategory"] ?? "0";
            //model.ParentId = Convert.ToInt32(parentId);
            model.Description = HttpContext.Current.Request.Params["ArticleCategoryDesc"];
            //model.InUserId = 0;
            //model.InDate = DateTime.Now;
            model.EditDate = DateTime.Now;
            model.EditUserId = 0;
            //model.DisplayOrder = OrderGenerator.NewOrder();
            //model.DataStatus = 1;
            return articleCategoryService.Update(model);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "删除栏目", HttpMethod = "post")]
        public int DeleteArticleCategoryById(int id)
        {
            // 删除栏目的同时 删除栏目下属的文章
            IArticleCategoryService articleCategoryService = new ArticleCategoryService();

            var articleCategory = articleCategoryService.GetArticleCategoryById(id);
            
            if(articleCategory!=null)
            {
                IArticleService articleService=new ArticleService();
                //WHERE Lft BETWEEN @MyLeft AND @MyRight;
                var all = articleCategoryService.GetAllArticleCategory().Where(s => s.Lft>=articleCategory.Lft && s.Lft <= articleCategory.Rgt);
                foreach (ArticleCategoryInfo articleCategoryInfo in all)
                {
                    articleService.DeleteByCategoryId(articleCategory.Id);
                }
            }

            return articleCategoryService.Delete(id);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "获取文章分页数据", HttpMethod = "get")]
        public JQGridDataResult GetArticles()
        {
            // 每页显示记录数
            int pageSize = int.Parse(HttpContext.Current.Request.QueryString["rows"]);
            // 当前页
            int pageIndex = int.Parse(HttpContext.Current.Request.QueryString["page"]);

            IArticleService articleService = new ArticleService();
            var data= articleService.GetArticles(pageSize, pageIndex, string.Empty);



            // 记录总数
            int rowCount = data.TotalCount;
            // 总页数
            int pageCount = rowCount % pageSize == 0 ? rowCount / pageSize : rowCount / pageSize + 1;

            var resultObj = new JQGridDataResult
            {
                // 总页数
                PageCount = pageCount,
                // 当前页
                PageIndex = pageIndex,
                // 总记录数
                Total = rowCount,
                // 数据
                Data = data
            };
            return resultObj;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "新增文章", HttpMethod = "get")]
        public int InsertArticle()
        {
            IArticleService articleService=new ArticleService();
            var model=new ArticleInfo();

            model.CategoryId = Convert.ToInt32(HttpContext.Current.Request.Params["ArticleCategory"]);
            model.Title = HttpContext.Current.Request.Params["Title"];
            model.SubTitle = HttpContext.Current.Request.Params["SubTitle"];
            model.Summary = HttpContext.Current.Request.Params["Summary"];
            model.Content = HttpContext.Current.Request.Params["Content"];
            model.Keywords = HttpContext.Current.Request.Params["Keywords"];
            model.MetaDesc = HttpContext.Current.Request.Params["MetaDesc"];
            model.Source = HttpContext.Current.Request.Params["Source"];
            model.AllowComments = true;//(int)HttpContext.Current.Request.Params["AllowComments"];
            model.Clicks = Convert.ToInt32(HttpContext.Current.Request.Params["Clicks"]??"0");
            model.ReadPassword = (string)HttpContext.Current.Request.Params["ReadPassword"];
            model.UserId = 0;
            model.DisplayOrder = OrderGenerator.NewOrder();
            model.PublishDate = Convert.ToDateTime(HttpContext.Current.Request.Params["PublishDate"]);
            model.InDate = DateTime.Now;
            model.EditDate = DateTime.Now;
            model.FocusPicture = HttpContext.Current.Request.Params["FocusPicture"];
            return articleService.Insert(model);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "修改文章", HttpMethod = "Post")]
        public int UpdateArticle()
        {
            IArticleService articleService = new ArticleService();
            var model = new ArticleInfo();
            model.Id = Convert.ToInt32(HttpContext.Current.Request.Params["ArticleId"]);
            model.CategoryId = Convert.ToInt32(HttpContext.Current.Request.Params["ArticleCategoryId"]);
            model.Title = HttpContext.Current.Request.Params["Title"];
            model.SubTitle = HttpContext.Current.Request.Params["SubTitle"];
            model.Summary = HttpContext.Current.Request.Params["Summary"];
            model.Content = HttpContext.Current.Request.Params["Content"];
            model.Keywords = HttpContext.Current.Request.Params["Keywords"];
            model.MetaDesc = HttpContext.Current.Request.Params["MetaDesc"];
            model.Source = HttpContext.Current.Request.Params["Source"];
            model.AllowComments = true;//(int)HttpContext.Current.Request.Params["AllowComments"];
            model.Clicks = Convert.ToInt32(HttpContext.Current.Request.Params["Clicks"] ?? "0");
            model.ReadPassword = HttpContext.Current.Request.Params["ReadPassword"];
            model.UserId = 0;
            model.DisplayOrder = OrderGenerator.NewOrder();
            model.PublishDate = Convert.ToDateTime(HttpContext.Current.Request.Params["PublishDate"]);
            model.InDate = DateTime.Now;
            model.EditDate = DateTime.Now;
            model.FocusPicture = HttpContext.Current.Request.Params["FocusPicture"];
            return articleService.Update(model);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "根据文章id删除文章", HttpMethod = "get")]
        public int DeleteArticleById(int id)
        {
            IArticleService articleService = new ArticleService();
            return articleService.Delete(id);
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "配置分类", HttpMethod = "get")]
        public object GetCommonList(string group,string defaultValue)
        {
            var data=ConfigHelper.ListConfig.GetListItems(group);
            if(defaultValue=="0")
            {
                if (data.Count(s => s.IsSelected == true) <= 0 && data.Count > 0)
                {
                    data.FirstOrDefault().IsSelected = true;
                }
            }
            else
            {
                if(data.FirstOrDefault(s=>s.Value==defaultValue)!=null)
                {
                    data.FirstOrDefault(s => s.Value == defaultValue).IsSelected = true;
                }
            }

            return data;
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "提交贷款", HttpMethod = "get")]
        public int InsertApplyForLoan(string userName, string cellPhone, long loan, string p1, string p2)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertApplyForLoan"))
            {
                cmd.SetParameterValue("@ApplyId",KeyGenerator.Instance.GetNextValue("ApplyForLoan"));
                cmd.SetParameterValue("@UserName", userName);
                cmd.SetParameterValue("@CellPhone", cellPhone);
                cmd.SetParameterValue("@Loan", loan);
                cmd.SetParameterValue("@LoanType", p1);
                cmd.SetParameterValue("@Mortgage", p2);
                cmd.SetParameterValue("@InDate", DateTime.Now);
                return cmd.ExecuteNonQuery();
            }
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "获取分页数据", HttpMethod = "get")]
        public object GetApplyForLoan()
        {
            DataTable dt;
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertApplyForLoan"))
            {
                cmd.CommandText = "select * from ApplyForLoan";
                dt = cmd.ExecuteDataTable();
            }


            var resultObj = new JQGridDataResult
            {
                
                // 总页数
                PageCount = 1,
                // 当前页
                PageIndex = 1,
                // 总记录数
                Total = dt.Rows.Count,
                // 数据
                Data = dt
            };
            return resultObj;
        }

    }

}