using MyDigitalShelf.model;
using MyDigitalShelf.Model.Storage;
using MyDigitalShelf.MyDigitalShelf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyDigitalShelf.Model.Service
{
    class CategoryDirectoryService
    {

        public async static Task<List<Category>> GetCategories()
        {
            var Service = new AzureServiceCategory();
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await Service.SyncAsync();
            }
            var Items = await Service.GetTable("9d573dfa-bb3f-4eae-ab63-95c126401fd2");
            return Items.ToList();
        }

        public async static Task CleanData()
        {
            var Service = new AzureService<Category>();
            await Service.CleanData();
        }

        public async static Task<List<Category>> GetCats()
        {
            List<Category> Categories;
            var URLWebAPI = "http://demos.ticapacitacion.com/cats";
            using (var Client = new System.Net.Http.HttpClient())
            {
                var JSON = await Client.GetStringAsync(URLWebAPI);
                Categories = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(JSON);
            }
            return Categories;
        }


        public static  CategoryDirectory LoadCategoryDirectory()
        {
            DatabaseManager dbManager = new DatabaseManager();
            ObservableCollection<Category> categoryList = new ObservableCollection<Category>(dbManager.GetAllItems<Category>());

            string[] categories = {"Vowel","Consonant","Alphabet"};

            CategoryDirectory categoryDirectory = new CategoryDirectory();

            if (categoryList!=null&&categoryList.Any())
            {
                categoryDirectory.CategoryList = categoryList;

            }
            else { 
                Random rdn = new Random(DateTime.Now.Millisecond);
                  
                for (int i = 0; i < categories.Length; i++)
                {
                    Category category    = new Category();
                    string key = rdn.Next(12384748,32384748).ToString();
                    category.Key         = key;
                    category.Value       = categories[i] ;
                    category.Description = categories[i];
                    categoryList.Add(category);
                    dbManager.SaveValue<Category>(category);
                }
                categoryDirectory.CategoryList = categoryList;
            }
            return categoryDirectory;
        }

        public static void saveCategory(Category category)
        {
            DatabaseManager dbManager = new DatabaseManager();
            Random rdn = new Random(DateTime.Now.Millisecond);
            string key = rdn.Next(12384748, 32384748).ToString();
            category.Key = key;
            dbManager.SaveValue<Category>(category);
        }
        
    }
}
