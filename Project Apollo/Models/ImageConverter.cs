using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Project_Apollo.Models {
	public class ImageConverter {

        public static byte[] convertPhotoToBytes(string photo)
        {
            photo = new Regex("^data:image\\/[a-z]+;base64,").Replace(photo, "");

            byte[] bytes = System.Convert.FromBase64String(photo);

            return bytes;
        }



        public static string convertPhotoToString(byte[] bytes)
        {
            var img = "";
            var base64 = Convert.ToBase64String(bytes);
            img = String.Format("data:image/gif;base64,{0}", base64);
            return img;
        }
    }
}