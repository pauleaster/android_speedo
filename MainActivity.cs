namespace speedo;
using Android.Util;
using Android.Graphics;
using Android.Views;

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
        var orientation = Resources.Configuration.Orientation;
        string toastMessage = "Current rotation: ";
        var display = WindowManager.DefaultDisplay;
        var rotation = display.Rotation;
        
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

        switch (orientation) {
        case Android.Content.Res.Orientation.Landscape:
            toastMessage += "Landscape";
            break;
        case Android.Content.Res.Orientation.Portrait:
            toastMessage += "Portrait";
            break;
        default:
            toastMessage += "Unknown";
            break;
        }

        Toast.MakeText(this, toastMessage, ToastLength.Short).Show();

        switch (rotation)
        {
            case SurfaceOrientation.Rotation0:
            case SurfaceOrientation.Rotation180:
                // Portrait orientation
                Toast.MakeText(this, "Portrait", ToastLength.Short).Show();
                break;
            case SurfaceOrientation.Rotation90:
            case SurfaceOrientation.Rotation270:
                // Landscape orientation
                Toast.MakeText(this, "Landscape", ToastLength.Short).Show();
                break;
        }
    }

    public override void OnConfigurationChanged(Configuration newConfig)
    {
        base.OnConfigurationChanged(newConfig);
        Toast.MakeText(this, "Rotation changed: " + newConfig.Orientation.ToString(), ToastLength.Short).Show();
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