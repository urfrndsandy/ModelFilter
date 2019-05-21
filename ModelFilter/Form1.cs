using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Tekla.Structures.Filtering;
using Tekla.Structures.Filtering.Categories;
using Tekla.Structures.Drawing;

namespace ModelFilter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*  Model model = new Model();
              BinaryFilterExpression filterExpression = new BinaryFilterExpression(new TemplateFilterExpressions.CustomString("PROFILE"),StringOperatorType.IS_EQUAL, new StringConstantFilterExpression("HEB200"));

            Tekla.Structures.Model.UI.ModelObjectSelector mos = new Tekla.Structures.Model.UI.ModelObjectSelector();

              ModelObjectEnumerator moe = model.GetModelObjectSelector().GetObjectsByFilter(filterExpression);*/

            StreamWriter sw = new StreamWriter(textBox1.Text);
            DrawingHandler Dh = new DrawingHandler();
            Drawing CurrentDrawing = Dh.GetActiveDrawing();
            DrawingObjectEnumerator Doe = CurrentDrawing.GetSheet().GetAllObjects();

            while (Doe.MoveNext())
            {
                Mark PartMark = Doe.Current as Mark; 
                if (PartMark == null) continue;

                if (PartMark.Attributes.Content.Count > 1)
                {
                    // PartMark.Delete();
                    DrawingObjectEnumerator Related = PartMark.GetRelatedObjects();

                    while (Related.MoveNext())
                    {
                        Part DrawingPart = Related.Current as Part;
                        if (DrawingPart == null) continue;
                        Tekla.Structures.Model.Beam ModelPart = new Tekla.Structures.Model.Beam();
                        ModelPart.Identifier = DrawingPart.ModelIdentifier;
                        string PropValue = string.Empty;
                        ModelPart.GetReportProperty("PART_POS", ref PropValue);
                        sw.WriteLine("'" + PropValue);
                    }
                }
            }

            sw.Close();

        }
    }
}
