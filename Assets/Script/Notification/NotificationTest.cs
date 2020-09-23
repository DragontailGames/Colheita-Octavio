using UnityEngine;
using System.Collections;
using System;


    public class NotificationTest : MonoBehaviour
    {
        /*
        1 - Stage
        2 - event
        3 - coffe full
        4 - OneWeek
        */

        TimeManager manager;

        long test = 1;

        void Awake()
        {
            
            LocalNotification.ClearNotifications();
            manager = GameObject.Find("TimerManager").GetComponent<TimeManager>();
            oneWeekAfter();
            stageEnd();
        }

        void Start()
        {
            Invoke("eventNotification",2f);           
        }

        public void eventNotification()
        {
            if(PlayerPrefs.HasKey("eventCallTime"))
            {
                TimeSpan msTime = DateTime.Parse(PlayerPrefs.GetString("eventCallTime")) - manager.currentTime();
                long aux = (long)msTime.TotalMilliseconds;
                LocalNotification.SendNotification(2, aux, "Novo Evento Disponivel", "Algum evento aconteceu com sua fazendo enquanto estava fora", new Color32(0xff, 0x44, 0x44, 255));
            }
        }

        public void oneWeekAfter()
        {
            TimeSpan aux = manager.currentTime().AddDays(7).TimeOfDay;
            LocalNotification.SendNotification(4, (long)aux.TotalMilliseconds, "Volte a Jogar", "A sua fazenda precisa de você, volte a jogar", new Color32(0xff, 0x44, 0x44, 255));
        }

        public void stageEnd()
        {
            if(PlayerPrefs.HasKey("stageEnd"))
            {
                TimeSpan msTime = DateTime.Parse(PlayerPrefs.GetString("stageEnd")) - manager.currentTime();
                long aux = (long)msTime.TotalMilliseconds;
                LocalNotification.SendNotification(1, aux, "Stage End", "O estagio atual esta completo volte a sua fazenda", new Color32(0xff, 0x44, 0x44, 255));
            }
        }

        void Update()
        {
            LocalNotification.CancelNotification(3);
            coffeFull();
        }

        public void coffeFull()
        {
            resourcesManager rManager = GameObject.Find("ResourcesManager").GetComponent<resourcesManager>();

            DateTime aux = (manager.currentTime().AddMinutes(15 * rManager.toFullCaffeine()));

            if(rManager.toFullCaffeine() >= 4)
            {
                LocalNotification.SendNotification(3, (long)aux.TimeOfDay.TotalMilliseconds, "Sua Cafeina esta Completa", "Você esta cheio de cafeina, volte a jogar", new Color32(0xff, 0x44, 0x44, 255));
            }
        }

        //Stardart

        public void Repeating()
        {
            LocalNotification.SendRepeatingNotification(1, 5000, 60000, "Title", "Long message text", new Color32(0xff, 0x44, 0x44, 255));
        }

        public void Stop()
        {
            LocalNotification.CancelNotification(1);
        }

        public void OnAction(string identifier)
        {
            Debug.Log("Got action " + identifier);
        }
    }

