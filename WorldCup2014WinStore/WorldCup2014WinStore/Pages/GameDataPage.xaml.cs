using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class GameDataPage : Page
    {
        #region Property

        #endregion

        #region Lifecycle

        public GameDataPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pageTitle.Show("赛程赛果");

            LoadScore();
            LoadGoal();
            LoadSchedule();
        }

        #endregion

        //progressbar.Visibility = Visibility.Visible;
        //progressbar.Visibility = Visibility.Collapsed;

        #region Score

        ListDataLoader<ScoreItemData> scoreLoader = new ListDataLoader<ScoreItemData>();

        private void LoadScore()
        {
            if (scoreLoader.Busy)
            {
                return;
            }

            progressbar.Visibility = Visibility.Visible;

            scoreLoader.Load("getscore", string.Empty, true, Constants.GAME_DATA_MODULE, Constants.SCORE_FILE_NAME,
                result =>
                {
                    scoreListBox.ItemsSource = result;
                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        #endregion

        #region Goal

        ListDataLoader<Goal> goalLoader = new ListDataLoader<Goal>();

        private void LoadGoal()
        {
            if (goalLoader.Busy)
            {
                return;
            }

            goalLoader.Load("getgoal", string.Empty, true, Constants.GAME_DATA_MODULE, Constants.GOAL_FILE_NAME,
                result =>
                {
                    foreach (var item in result)
                    {
                        item.Index = result.IndexOf(item) + 1;
                    }

                    goalListBox.ItemsSource = result;
                    goalScrollViewer.ChangeView(null, 0, null);
                });
        }

        #endregion

        #region Schedule

        ListDataLoader<Schedule> scheduleLoader = new ListDataLoader<Schedule>();

        private void LoadSchedule()
        {
            if (scheduleLoader.Busy)
            {
                return;
            }

            scheduleLoader.Load("getschedule", string.Empty, true, Constants.GAME_DATA_MODULE, Constants.SCHEDULE_FILE_NAME,
                result =>
                {
                    scheduleListBox.ItemsSource = result;
                    scheduleScrollViewer.ChangeView(null, 0, null);
                });
        }

        #endregion

    }
}
