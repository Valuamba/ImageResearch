using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.Helpers
{
    public static class DialogHelper
    {
        public static bool ShowOpenFileDialog(out FileInfo file)
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = "All Images|*.mim;*.bmp;*.png;*.jpg;*.jpeg;*.gif;*.tif|All Files|*.*";
            dialog.Multiselect = false;

            var result = dialog.ShowDialog();

            if (result == true)
            {
                file = new FileInfo(dialog.FileName);

                return true;
            }
            else
            {
                file = null;
                return false;
            }
        }

        public static bool ShowSaveFileDialog(out FileInfo file)
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = "My Image|*.mim|Bitmap|*.bmp|PNG Image|*.png|JPEG Image|*.jpg|GIF Image|*.gif|TIFF Image|*.tif";
            dialog.AddExtension = true;

            var result = dialog.ShowDialog();

            if (result == true)
            {
                file = new FileInfo(dialog.FileName);
                return true;
            }
            else
            {
                file = null;
                return false;
            }
        }
    }
}
