using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace HelloApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string[] stringArray = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] intArray = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                if (int.TryParse(stringArray[i], out int result))
                {
                    intArray[i] = result;
                }
            }

            CombSort combSort = new CombSort();
            combSort.array = intArray;
            combSort.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] array = GetRandomArray();
            String arrayString = string.Join(", ", array);

            textBox1.Text = arrayString;
        }

        static int[] GetRandomArray()
        {
            var r = new Random();
            var outputArray = new int[10];
            for (var i = 0; i < outputArray.Length; i++)
            {
                outputArray[i] = r.Next(0, 12);
            }

            return outputArray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string[] stringArray = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] intArray = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                if (int.TryParse(stringArray[i], out int result))
                {
                    intArray[i] = result;
                }
            }

            RadixSort radixSort = new RadixSort();
            radixSort.array = intArray;
            radixSort.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string[] stringArray = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] intArray = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                if (int.TryParse(stringArray[i], out int result))
                {
                    intArray[i] = result;
                }
            }

            InsertionSort insertionSort = new InsertionSort();
            insertionSort.array = intArray;
            insertionSort.Show();
        }
    }
}