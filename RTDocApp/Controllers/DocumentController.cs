using Microsoft.AspNetCore.Mvc;

namespace RTDocApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {

        string dir = Path.Join(Directory.GetCurrentDirectory(), "Storage", "Files");

        [HttpGet]
        public List<CustomData> GetAllDocuments()
        {
            
            DirectoryInfo? filesDir = new DirectoryInfo(dir);

            List<CustomData> dataList = new();
            if (filesDir != null) 
            { 
                FileInfo[] files = filesDir.GetFiles();
                int id = 0;
                foreach (FileInfo file in files) 
                { 
                    
                    dataList.Add(new CustomData() 
                    { 
                        Id = id, 
                        Path = file.FullName, 
                        DocName = file.Name 
                    });
                    id++;
                }
            }
            
            

            return dataList;

        }

        [HttpPost]
        public int CreateDocument(int id)
        {

            FileStream fs = null;
            DirectoryInfo di = new DirectoryInfo(dir);
            foreach (FileInfo files in di.GetFiles()) 
            {
                if (files.Name == $"Document{id}.txt") 
                {
                    return -1;
                }
            }
            fs = new FileStream(Path.Join(dir, $"Document{id}.txt"), FileMode.Create);

            if (fs != null) 
            {

                fs.Close();
                return id;
            }
            return -1;
        }

        [HttpDelete]
        public int DeleteDocument(int id) 
        { 
            DirectoryInfo di = new DirectoryInfo(dir);
            foreach (FileInfo files in di.GetFiles())
            {
                if (files.Name == $"Document{id}.txt")
                {
                    files.Delete(); 
                    return 1;
                }
            }
            return -1;
        }
    }
  

    public class CustomData
    {
        public int Id { get; set; }
        public string? Path { get; set; }
        public string? DocName { get; set; }
    }
}
