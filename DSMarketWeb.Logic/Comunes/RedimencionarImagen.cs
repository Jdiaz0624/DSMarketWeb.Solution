using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace DSMarketWeb.Logic.Comunes
{
    public static class RedimencionarImagen
    {
        public static System.Drawing.Image Redireccionar(System.Drawing.Image ImagenOriginal, int Alto) {

            var Radio = (double)Alto / ImagenOriginal.Height;
            var NuevoAncho = (int)(ImagenOriginal.Width * Radio);
            var NuevoAlto = (int)(ImagenOriginal.Height * Radio);
            var NuevaImagenRedimencionada = new Bitmap(NuevoAncho, NuevoAlto);
            var G = Graphics.FromImage(NuevaImagenRedimencionada);
            G.DrawImage(ImagenOriginal, 0, 0, NuevoAncho, NuevoAlto);
            return NuevaImagenRedimencionada;
        }
    }
}
