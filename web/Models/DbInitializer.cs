using System.Reflection.Metadata;
using web.Models;
using web.Models.Entities;

namespace web.Models
{
    public static class DbInitializer
    {
        public async static Task Init(ApplicationContext db)
        {
            {
                // comment this
                // await db.Database.EnsureDeletedAsync();
                // await db.Database.EnsureCreatedAsync();

                if (!db.Items.Any())
                {
					List<Category> categories = new List<Category>
					{
						new Category
	{
		Name = "Electronics",
		Items = new List<Item>
		{
			new Item
			{
				Title = "Laptop",
				Description = "High performance gaming laptop",
				Images = new List<string> { "/images/laptop1.jpeg", "/images/laptop2.jpeg" },
				Owner = "Alice",
				PriceEth = 1.25m,
				Stock = 10,
			},
			new Item
			{
				Title = "Smartphone",
				Description = "Latest flagship smartphone",
				Images = new List<string> { "/images/phone1.jpeg" },
				Owner = "Alice",
				PriceEth = 0.75m,
				Stock = 20
			}
		}
	},
						new Category
	{
		Name = "Books",
		Items = new List<Item>
		{
			new Item
			{
				Title = "Fantasy Novel",
				Description = "An epic tale of magic and dragons",
				Images = new List<string> { "/images/novel.jpeg" },
				Owner = "Bob",
				PriceEth = 0.05m,
				Stock = 100
			},
			new Item
			{
				Title = "Cookbook",
				Description = "Over 100 delicious recipes",
				Images = new List<string> { "/images/cookbook.jpeg" },
				Owner = "Bob",
				PriceEth = 0.08m,
				Stock = 50
			}
		}
	}
					};

					db.Categories.AddRange(categories);
					await db.SaveChangesAsync();

					var items = db.Items.ToList();

					var review = new Review
					{
						Content = "This laptop is amazing!",
						Rating = 9,
						Item = items[0],
						Replies = new List<Comment>()
					};

					var comment1 = new Comment
					{
						Content = "Glad to hear! How long have you been using it?",
						Review = review
					};

					var comment1Reply = new Comment
					{
						Content = "About 2 weeks, still loving it!",
						Review = review,
						Parent = comment1
					};

					review.Replies.Add(comment1);
					comment1.Replies.Add(comment1Reply);

					db.Reviews.Add(review);
					await db.SaveChangesAsync();

				}
            }
        }
    }
}
