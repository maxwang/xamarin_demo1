using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace ActivityLifeCycleDemo
{
    [Activity(Label = "ActivityLifeCycleDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int _counter = 0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.btnSecondActivity).Click += (sender, args) =>
            {
                var intent =new Intent(this, typeof(SecondActivity));
                StartActivity((intent));
            };

            if (bundle != null)
            {
                _counter = bundle.GetInt("click_count", 0);
            }

            var clickButton = FindViewById<Button>(Resource.Id.btnClickButton);
            clickButton.Text = _counter.ToString();
            clickButton.Click += (sender, e) =>
            {
                _counter++;
                clickButton.Text = _counter.ToString();
            };

            FindViewById<TextView>(Resource.Id.txtView).Text += "A OnCreate";

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("click_count", _counter);

            FindViewById<TextView>(Resource.Id.txtView).Text += "A OnSaveInstanceState";

            base.OnSaveInstanceState(outState);
        }

        protected override void OnDestroy()
        {
            FindViewById<TextView>(Resource.Id.txtView).Text += "A OnDestroy";
            base.OnDestroy();
        }

        protected override void OnPause()
        {
            FindViewById<TextView>(Resource.Id.txtView).Text += "A OnPause";
            base.OnPause();
        }


        protected override void OnResume()
        {
            FindViewById<TextView>(Resource.Id.txtView).Text += "A OnResume";
            base.OnResume();
        }

        protected override void OnStart()
        {
            FindViewById<TextView>(Resource.Id.txtView).Text += "A OnStart";
            base.OnStart();
        }


        protected override void OnStop()
        {
            FindViewById<TextView>(Resource.Id.txtView).Text += "A OnStop";
            base.OnStop();
        }
        
    }
}

