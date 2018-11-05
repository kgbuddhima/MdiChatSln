using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.Services
{
    public interface IFileService
    {
        Task<string> SaveByteArrayAsImageFile(byte[] imageData,string fileName);
    }
}
