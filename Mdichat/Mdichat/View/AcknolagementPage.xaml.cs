using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mdichat.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AcknolagementPage : ContentPage
	{
		public AcknolagementPage ()
		{
			InitializeComponent ();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StringBuilder builder = new StringBuilder();
            builder.Append("I have taken efforts in this project. However, it would not have been possible without the kind support and help of many individuals and organizations. I would like to extend my sincere thanks to all of them.");
            builder.Append("I am highly indebted to (Name of your Organization Guide) for their guidance and constant supervision as well as for providing necessary information regarding the project & also for their support in completing the project.");
            builder.Append("I would like to express my gratitude towards my parents & member of (Organization Name)for their kind co-operation and encouragement which help me in completion of this project.");
            builder.Append("I would like to express my special gratitude and thanks to industry persons for giving me such attention and time.");
            builder.Append("My thanks and appreciations also go to my colleague in developing the project and people who have willingly helped me out with their abilities.");
            builder.Append("This research was supported/partially supported by [Name of Foundation, Grant maker, Donor]. We thank our colleagues from [Name of the supporting institution] who provided insight and expertise that greatly assisted the research, although they may not agree with all of the interpretations/conclusions of this paper.");
            builder.Append("We thank [Name Surname, title] for assistance with [particular technique, methodology], and [Name Surname, position, institution name] for comments that greatly improved the manuscript.");
            builder.Append("We would also like to show our gratitude to the (Name Surname, title, institution) for sharing their pearls of wisdom with us during the course of this research, and we thank 3 “anonymous” reviewers for their so-called insights. We are also immensely grateful to (List names and positions) for their comments on an earlier version of the manuscript, although any errors are our own and should not tarnish the reputations of these esteemed persons.");
            builder.Append("With acknowledgement letter, used in business purposes you should clearly confirm that facts stated are true, i.e. that communicated information is truthful. To simplify a bit, this means that is you want to acknowledge receipt of the order you must quote and reference buyer’s request for the order, and then confirm it or make certain amendments. In this letter you can also give a timeline of activities that will proceed as a result of the order acknowledgement. Or in another case if you acknowledge the receipt of someone job application, you should state that application is being received, and briefly indicate future actions in regard to the received application.");
            builder.Append("Acknowledgement letter is very short business letter, and is intended to communicate brief and clear message. It is quite common to use this letter if you are not aware at the time of future developments in regard to someone’s query.");
            lblAck.Text = builder.ToString();
        }
    }
}