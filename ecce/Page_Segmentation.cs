using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ecce.Form1;
using Emgu.CV.Util;
using System.Diagnostics;

namespace ecce
{
    internal class Page_Segmentation
    {

        public int param_heigth = 0;
        public int param_width = 0;
        public int param_des = 1;

        public VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        List<BoxandContour> lst_cont_plus_rect = new List<BoxandContour>();
        public List<Segmentation_box> lst_segboxes = new List<Segmentation_box>();
        public Image<Bgr, Byte> showimg { get; set; }


        public int[] bottom_line;
        int _box_column;
        int _box_row;
        int _index_row;

        public Image<Bgr, byte> xblockSegmentation(Image<Gray, byte> img, int width, int height, int destroy_rec)
        {
            try
            {
                lst_cont_plus_rect.Clear();
                lst_segboxes.Clear();
                contours.Clear();
                param_heigth = height;
                param_width = width;
                param_des = destroy_rec;
               Mat kernel = Mat.Ones(param_heigth, param_width, DepthType.Cv8U, 1);

                var binary = img.MorphologyEx(MorphOp.Erode, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0, 0, 0));
                CvInvoke.BitwiseNot(binary, binary);
                var h = new Mat();
                CvInvoke.FindContours(binary, contours, h, RetrType.External, ChainApproxMethod.ChainApproxSimple);
               
                for (int i = 0; i < contours.Size; i++)
                {
                    var bbox = CvInvoke.BoundingRectangle(contours[i]);
                    int area = bbox.Width * bbox.Height;

                    if (area > destroy_rec)
                    {
                        BoxandContour hope = new BoxandContour();
                        hope.rect = bbox;
                        hope.index_contour = i;
                        lst_cont_plus_rect.Add(hope);
                    }
                }
                if (lst_cont_plus_rect.Count > 0)
                {
                    lst_cont_plus_rect = lst_cont_plus_rect.OrderBy(x => x.rect.Y).ThenBy(x => x.rect.X).ToList();
                    sorting_routine();
                    return draw_boundingbox(img);
                }
                else
                {
                    return img.Convert<Bgr, byte>();
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
       
        public Image<Bgr, byte> draw_boundingbox(Image<Gray, byte> img)
        {
            showimg = new Image<Bgr, byte>(img.Width, img.Height);
            showimg = img.Convert<Bgr, byte>();

            if (lst_segboxes.Count > 0)
            {

                for (int i = 0; i < lst_cont_plus_rect.Count; i++)
                {
                    CvInvoke.Rectangle(showimg, lst_segboxes[i].rect, new MCvScalar(200, 205, 0), 1);
                    CvInvoke.DrawContours(showimg, contours, lst_segboxes[i].index_contour, new MCvScalar(50, 50, 200), 1);
                    CvInvoke.PutText(showimg, i.ToString(), new Point((lst_segboxes[i].rect.X + lst_segboxes[i].rect.Width / 2), lst_segboxes[i].rect.Y), FontFace.HersheyPlain, 2, new MCvScalar(20, 50, 255));
                }
            }
            return showimg;
        }



        private void sorting_routine()
        {
            List<BoxandContour>[] temp_list = new List<BoxandContour>[lst_cont_plus_rect.Count()];
            temp_list[0] = new List<BoxandContour>();
            temp_list[0].Add(lst_cont_plus_rect[0]);
            int chckbottom = temp_list[0][0].rect.Bottom;

            int x = 0;
            for (int i = 1; i < lst_cont_plus_rect.Count; i++)
            {
                if (chckbottom > (lst_cont_plus_rect[i].rect.Top + 5))
                {
                    temp_list[x].Add(lst_cont_plus_rect[i]);
                    if (lst_cont_plus_rect[i].rect.Bottom > chckbottom) { chckbottom = lst_cont_plus_rect[i].rect.Bottom; }
                }
                else
                {
                    x++;
                    temp_list[x] = new List<BoxandContour>();
                    temp_list[x].Add(lst_cont_plus_rect[i]);
                    chckbottom = lst_cont_plus_rect[i].rect.Bottom;
                }
            }
            
            bottom_line = new int[(x + 1)];
            _box_column = 0;
            _box_row = x + 1;
            List<BoxandContour>[] temp_list2 = new List<BoxandContour>[temp_list.Count()];
            for (int i = 0; i <= x; i++)
            {
                temp_list2[i] = temp_list[i].OrderByDescending(x => x.rect.Bottom).ToList();
                bottom_line[i] = temp_list2[i][0].rect.Bottom;
                temp_list[i] = temp_list[i].OrderBy(x => x.rect.X).ToList();
                for (int y = 0; y < temp_list[i].Count(); y++)
                {
                    lst_segboxes.Add(new Segmentation_box() { rect = temp_list[i][y].rect, column = i, box_row = y, index_contour = temp_list[i][y].index_contour });
                    if (y > _box_column)
                    {

                        _box_column = y;
                        _index_row = i;
                    }
                }
            }
            _box_column++;
        }

         

        public int starting_point()
        {
            int startingpoint = 5000;

            for (int i = 0; i < lst_segboxes.Count(); i++)
            {

                if (startingpoint > lst_segboxes[i].rect.X)
                {
                    startingpoint = lst_segboxes[i].rect.X;
                }
            }
            return startingpoint;
        }
    }
}
