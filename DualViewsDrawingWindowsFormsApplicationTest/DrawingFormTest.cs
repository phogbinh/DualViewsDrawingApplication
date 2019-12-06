using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;


namespace DualViewsDrawingWindowsFormsApplicationTest
{
    [CodedUITest]
    public class DrawingFormTest
    {
        private const string DRAWING_FORM_TITLE = "DrawingForm";
        private const string RECTANGLE_BUTTON_TEXT = "Rectangle";
        private const string LINE_BUTTON_TEXT = "Line";
        private const string CLEAR_BUTTON_TEXT = "Clear";
        private const string CANVAS_ACCESSIBLE_NAME = "CanvasPanel";

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            string filePath = Directory.GetCurrentDirectory() + @"\..\..\..\DualViewsDrawingWindowsFormsApplication\bin\Debug\DualViewsDrawingWindowsFormsApplication.exe";
            Robot.Initialize(filePath, DRAWING_FORM_TITLE);
        }

        /// <summary>
        /// Tests the click all buttons.
        /// </summary>
        [TestMethod()]
        public void TestClickAllButtons()
        {
            Robot.AssertButtonEnable(RECTANGLE_BUTTON_TEXT, true);
            Robot.AssertButtonEnable(LINE_BUTTON_TEXT, true);
            Robot.AssertButtonEnable(CLEAR_BUTTON_TEXT, true);
            Robot.ClickButton(RECTANGLE_BUTTON_TEXT);
            Robot.AssertButtonEnable(RECTANGLE_BUTTON_TEXT, false);
            Robot.AssertButtonEnable(LINE_BUTTON_TEXT, true);
            Robot.AssertButtonEnable(CLEAR_BUTTON_TEXT, true);
            Robot.ClickButton(LINE_BUTTON_TEXT);
            Robot.AssertButtonEnable(RECTANGLE_BUTTON_TEXT, true);
            Robot.AssertButtonEnable(LINE_BUTTON_TEXT, false);
            Robot.AssertButtonEnable(CLEAR_BUTTON_TEXT, true);
            Robot.ClickButton(CLEAR_BUTTON_TEXT);
            Robot.AssertButtonEnable(RECTANGLE_BUTTON_TEXT, true);
            Robot.AssertButtonEnable(LINE_BUTTON_TEXT, true);
            Robot.AssertButtonEnable(CLEAR_BUTTON_TEXT, true);
        }

        /// <summary>
        /// Tests the draw house.
        /// </summary>
        [TestMethod()]
        public void TestDrawHouse()
        {
            // Draws a block.
            DrawRectangle(CANVAS_ACCESSIBLE_NAME, 250, 150, 550, 300);
            // Draws a roof.
            DrawLine(CANVAS_ACCESSIBLE_NAME, 250, 150, 400, 75);
            DrawLine(CANVAS_ACCESSIBLE_NAME, 400, 75, 550, 150);
            // Draws a door.
            DrawRectangle(CANVAS_ACCESSIBLE_NAME, 375, 200, 425, 300);
            // Draws two windows.
            DrawRectangle(CANVAS_ACCESSIBLE_NAME, 300, 175, 325, 200);
            DrawRectangle(CANVAS_ACCESSIBLE_NAME, 475, 175, 500, 200);
            // Clears the house.
            Robot.ClickButton(CLEAR_BUTTON_TEXT);
            Robot.AssertButtonEnable(RECTANGLE_BUTTON_TEXT, true);
            Robot.AssertButtonEnable(LINE_BUTTON_TEXT, true);
            Robot.AssertButtonEnable(CLEAR_BUTTON_TEXT, true);
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        private void DrawRectangle(string name, int x1, int y1, int x2, int y2)
        {
            Robot.ClickButton(RECTANGLE_BUTTON_TEXT);
            UITestControl canvas = Robot.FindPanel(name);
            Mouse.StartDragging(canvas, new Point(x1, y1));
            Mouse.StopDragging(canvas, new Point(x2, y2));
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        private void DrawLine(string name, int x1, int y1, int x2, int y2)
        {
            Robot.ClickButton(LINE_BUTTON_TEXT);
            UITestControl canvas = Robot.FindPanel(name);
            Mouse.StartDragging(canvas, new Point(x1, y1));
            Mouse.StopDragging(canvas, new Point(x2, y2));
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup()]
        public void CleanUp()
        {
            Robot.CleanUp();
        }
    }
}
