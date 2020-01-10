using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace EnvDotNetPomodoro.Model
{
    public class PomodoroClock {
        private const int CLOCK_TIMER_WORK = 25 * 60 * 1000;
        private const int CLOCK_TIMER_BREAK = 5 * 60 * 1000;
        private const int CLOCK_TIMER_PAUSE = 15 * 60 * 1000;
        public CountDownTimer countDownTimer { get; set; }
        public int[] config;
        public int currentIndexTimer { get; set; }
        public int priorite { get; set; }
        public string sujet { get; set; }
        public string client { get; set; }
        public string[] tag { get; set; }

        public string tagAsString{
            get {
                return string.Join(" - ", tag);
            }
            }

        public PomodoroClock(string sujet,string client, int priorite, string tags) {
            config = new int[]{CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_PAUSE};
            currentIndexTimer = 0;
            this.sujet = sujet;
            this.client = client;
            this.priorite = priorite;
            tag = tags.Split(' ');
            countDownTimer = new CountDownTimer(config[currentIndexTimer]);
        }
        public void start() {
            countDownTimer.startTimer();
        }
        public void stop() {
            countDownTimer.stopTimer();
        }
        public void play() {
            countDownTimer.playTimer();
        }
        public void pause() {
            countDownTimer.pauseTimer();
        }
        public void next() {
            currentIndexTimer++;
            if (currentIndexTimer > 7) { 
                currentIndexTimer = 0;
                countDownTimer.stopTimer();
            }

            countDownTimer.nextTimer(config[currentIndexTimer]);
        }
    }

    public class CountDownTimer {
        private int? _timer;
        private Boolean start = false;
        public event EventHandler OnChange;
        public int? timer {
            get { return _timer; }
            set { 
                _timer = value;
                if (OnChange!=null) {
                    OnChange(this, new EventArgs());
                }            
            }
        }
        public CountDownTimer(int delay) {
            timer = delay;
            Thread t = new Thread(new ThreadStart(tikeTimer));
            t.Start();
        }
        public void startTimer() {
            start = true;
        }
        public void tikeTimer() {
            while (true) {
                if (start)
                    timer-=1000;
                Thread.Sleep(1000);
            }
        }
        public void stopTimer() {
            start = false;
            timer = 0;
        }
        public void pauseTimer() {
            start = false;
        }
        public void playTimer() {
            start = true;
        }
        public void nextTimer(int delay) {
            timer = delay;
            start = true;
        }
        public string getTimerValue() {
            int? seconde = (timer / 1000) % 60;
            int? minute = ((timer - seconde)/1000) / 60;
            string sminute = minute.ToString().Length < 2 ? '0' + minute.ToString() : minute.ToString();
            string sseconde= seconde.ToString().Length < 2 ? '0' + seconde.ToString() : seconde.ToString();
            return sminute+':'+sseconde;
        }
        public Boolean finish() {
            if (timer < 0)
                return true;
            return false;
        }
    }
}
