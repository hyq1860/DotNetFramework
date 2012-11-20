Create table Product
(
ProductId char(32),--产品id
Name varchar(150),--商品名称
Url varchar(200),--采集url
CategoryId int,--分类
Supplier int,--供货商
Inventory int,--库存
CommentNumber int,--评价数
InDate Datetime,--录入时间
EditDate Datetime,--编辑时间
)

Create table ProductPriceHistory
(
ProdoutId char(32),--产品guid
GatherDate datetime,--采集时间
Price Decimal,--价格
PriceImage varchar(255),--价格图片 每期的地址要不一致
PriceCheck Decimal,--价格辅助
PromotionInfo varchar(500)--促销信息
)

Create table ProductCategory
(
CategoryId varchar(32),
Name varchar(100),
ParentId varchar(32),
Url varchar(200)
)

Create table ProductCategoryPath
(
[Ancestor] [varchar](32) NOT NULL,
[Descendant] [varchar](32) NOT NULL
)

--采集配置
Create table GetherSite
(
SiteId int,
Name varchar(50)
)
Create Table GatherConfig
(

)