namespace counter;
using Android.Util;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    int count = 0;

    private (int, int) GetPercentageScreenSizeInSp(int p)
    {
        DisplayMetrics displayMetrics = Resources.DisplayMetrics;
        int screenWidthInSp = (int)(displayMetrics.WidthPixels / displayMetrics.ScaledDensity * p * 0.01);
        int screenHeightInSp = (int)(displayMetrics.HeightPixels / displayMetrics.ScaledDensity * p * 0.01);
        return (screenWidthInSp, screenHeightInSp);
    }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);

        // Calculate the font size in pixels based on the screen size
        int percentageFontWidth = Resources.GetInteger(Resource.Integer.percentage_font_width);

        (int screenWidthPercentageInSp, int screenHeightPercentageInSp) = GetPercentageScreenSizeInSp(percentageFontWidth);


        // Find the TextView with ID "myTextView" and assign it to the myTextView variable
        TextView myTextView = FindViewById<TextView>(Resource.Id.myTextView);

        // Set the text of the TextView to the initial count value
        if (myTextView != null)
        {
            myTextView.Text = count.ToString();
            myTextView.SetTextSize(ComplexUnitType.Sp, screenWidthPercentageInSp);
        }

        // Attach the OnClick event handler to the TextView
        myTextView.Click += MyTextView_Click;
    }

    // Define the OnClick event handler
    private void MyTextView_Click(object sender, EventArgs e)
    {
    // Increment the count value and update the TextView
        count++;
        TextView myTextView = sender as TextView;
        if (myTextView != null)
        {
            myTextView.Text = count.ToString();
        }
        // Force the screen to refresh
        Window.DecorView.PostInvalidate();
    }
}