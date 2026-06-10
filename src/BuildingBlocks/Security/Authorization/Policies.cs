namespace StarterKit.Api.BuildingBlocks.Security.Authorization;
public static class Roles { public const string Admin="Admin"; public const string User="User"; }
public static class Permissions { public const string ProductsManage="products.manage"; public const string OrdersManage="orders.manage"; }
public static class Policies { public const string CanManageProducts="CanManageProducts"; public const string AdminOnly="AdminOnly"; }
