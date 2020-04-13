namespace FamilyKitchen.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Common.Models;
    using FamilyKitchen.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<FamilyKitchenUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Allergen> Allergens { get; set; }

        public DbSet<FoodResourceAllergen> FoodResourcesAllergens { get; set; }

        public DbSet<NutritionDeclaration> NutritionDeclarations { get; set; }

        public DbSet<FoodResource> FoodResources { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<FoodResourceRecipe> FoodResourcesRecipes { get; set; }

        public DbSet<ShopProduct> ShopProducts { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartShopProduct> ShoppingCartsShopProducts { get; set; }

        public DbSet<ShopProductSubCategory> ShopProductsSubCategories { get; set; }

        public DbSet<Family> Families { get; set; }

        public DbSet<FamilyKitchenUserShoppingCart> FamilyKitchenUsersShoppingCarts { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            builder.Entity<FoodResourceAllergen>()
                .HasKey(pa => new { pa.FoodResourceId, pa.AllergenId });

            builder.Entity<FoodResourceAllergen>()
                .HasOne(pa => pa.FoodResource)
                .WithMany(p => p.FoodResourcesAllergens)
                .HasForeignKey(pa => pa.FoodResourceId);

            builder.Entity<FoodResourceAllergen>()
                .HasOne(pa => pa.Allergen)
                .WithMany(p => p.FoodResourcesAllergens)
                .HasForeignKey(pa => pa.AllergenId);

            builder.Entity<FoodResource>()
                .HasOne(fr => fr.NutritionDeclaration)
                .WithMany(n => n.FoodResources)
                .HasForeignKey(fr => fr.NutritionDeclarationId);

            builder.Entity<FoodResourceRecipe>()
              .HasKey(frr => new { frr.FoodResourceId, frr.RecipeId });

            builder.Entity<FoodResourceRecipe>()
                .HasOne(frr => frr.FoodResource)
                .WithMany(fr => fr.FoodResourcesRecipes)
                .HasForeignKey(frr => frr.FoodResourceId);

            builder.Entity<FoodResourceRecipe>()
                .HasOne(frr => frr.Recipe)
                .WithMany(fr => fr.FoodResourcesRecipes)
                .HasForeignKey(frr => frr.RecipeId);

            builder.Entity<ShoppingCartShopProduct>()
                .HasKey(sc => new { sc.ShoppingCartId, sc.ShopProductId });

            builder.Entity<ShoppingCartShopProduct>()
               .HasOne(sc => sc.ShopProduct)
               .WithMany(sp => sp.ShoppingCartsShopProducts)
               .HasForeignKey(sc => sc.ShopProductId);

            builder.Entity<ShoppingCartShopProduct>()
                .HasOne(sc => sc.ShoppingCart)
                .WithMany(sp => sp.ShoppingCartsShopProducts)
                .HasForeignKey(sc => sc.ShoppingCartId);

            builder.Entity<ShopProductSubCategory>()
                .HasKey(rs => new { rs.ShopProductId, rs.SubCategoryId });

            builder.Entity<ShopProductSubCategory>()
                .HasOne(rs => rs.ShopProduct)
                .WithMany(r => r.ShopProductsSubCategories)
                .HasForeignKey(rs => rs.ShopProductId);

            builder.Entity<ShopProductSubCategory>()
                .HasOne(rs => rs.SubCategory)
                .WithMany(r => r.ShopProductsSubCategories)
                .HasForeignKey(rs => rs.SubCategoryId);

            builder.Entity<Family>()
                .HasMany(f => f.FamilyMembers)
                .WithOne(fm => fm.Family)
                .HasForeignKey(fm => fm.FamilyId);

            builder.Entity<FamilyKitchenUserShoppingCart>()
               .HasKey(usc => new { usc.UserId, usc.ShoppingCartId });

            builder.Entity<FamilyKitchenUserShoppingCart>()
                .HasOne(usc => usc.User)
                .WithMany(u => u.FamilyKitchenUsersShoppingCarts)
                .HasForeignKey(usc => usc.UserId);

            builder.Entity<FamilyKitchenUserShoppingCart>()
                .HasOne(usc => usc.ShoppingCart)
                .WithMany(u => u.FamilyKitchenUsersShoppingCarts)
                .HasForeignKey(usc => usc.ShoppingCartId);

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
