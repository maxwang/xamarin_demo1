using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using static Android.Locations.GpsStatus;
using System.Collections.Generic;

namespace AndroidDemo
{
    [Activity(Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private static readonly List<string> _phoneNumbers = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            Button callHistoryButton = FindViewById<Button>(Resource.Id.CallHistory);
            callButton.Enabled = false;

            // Add code to translate number
            string translatedNumber = string.Empty;

            callHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CallHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", _phoneNumbers);
                StartActivity(intent);
            };

            translateButton.Click += (object sender, System.EventArgs e) =>
            {
                translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);

                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage($"Call {translatedNumber} ?");
                callDialog.SetNeutralButton("Call", delegate
                {
                    _phoneNumbers.Add(translatedNumber);
                    callHistoryButton.Enabled = true;

                    var callIntenet = new Intent(Intent.ActionCall);
                    callIntenet.SetData(Android.Net.Uri.Parse($"tel: {translatedNumber}"));
                    StartActivity(callIntenet);
                });

                callDialog.SetNegativeButton("Cancel", delegate { });
                callDialog.Show();
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = false;

                }
                else
                {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;

                }


            };
        }


        protected override void OnResume()
        {
            base.OnResume();
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            callButton.Text = "OnResume!";
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            callButton.Text = "Restored!";
        }
    }
}

