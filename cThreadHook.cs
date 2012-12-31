using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.IO.Pipes;


namespace ActionLogger
{
    /// <summary>
    /// Класс, который запускает прослушивание и обрабатывает данные от перехватчика событий. В будущем события будут 
    /// отлавливаться через отдельную DLL,которая будет разделять адресное пространство целевого процесса окна,
    /// и пересылать сериализованные данные через механизм Pipe'ов или Memory Mapped Files. Это будет выполняться в отдельном потоке,
    /// поэтому он и назван ThreadHook.
    /// </summary>
    class cThreadHook
    {
        private formStart FormControl = null;
       
        private HookClass hcListener = null;
        private IntPtr TargetWnd = IntPtr.Zero;

        public cThreadHook(formStart frm, IntPtr Target)
        {
            FormControl = frm;
            /*1st*/
          
          
            /*2nd*/ 
            hcListener = new HookClass(Target);
            this.TargetWnd = Target;
           
        }

        ~cThreadHook()
        {
            if (hcListener.Installed)
                Stop();
        }

        public void Start()
        {
            hcListener.SetHook(true);            
        }

        public void Stop()
        {

            hcListener.SetHook(false);
         
        }


        /// <summary>
        /// Обрабатывает событие по появлению нового элемента. Добавляет новый элемент в коллекцию и вызывает обработчик на форме
        /// </summary>
        public void Add(object sender, EventArgs ea)
        {
            UserAction uact = ActionHost.Actions.Last<UserAction>();
            System.Windows.Forms.Message mes = ActionHost.Actions.Last<UserAction>().EventCode;

            if (mes.WParam.ToInt32() == ucActionInfo.MOUSE_WPARAM) //TODO Сделаь switch по message_code
            {
                uact.MouseCoords = Func.ParseLParamToPoint(mes.LParam);
            }

            if (ActionHost.Actions.Last<UserAction>().EventCode.HWnd != FormControl.Handle)
                FormControl.Invoke(FormControl.intercept,new object[2]);
        }

        /// <summary>
        /// Устанавливает подопытное окно
        /// </summary>
        public void SetTargetWnd()
        {
            DisableFindingHook();
            FormControl.Invoke(FormControl.setedittext);
        }

        /// <summary>
        /// Включить поток поиска подопытного окна
        /// </summary>
        public void EnableFindingHook()
        {

            ActionHost.OnTargetWindowPicked += SetTargetWnd;
            hcListener.SetPickHook();
        
        }

        /// <summary>
        /// Отключить поток поиска подопытного окна
        /// </summary>
        public void DisableFindingHook()
        {
            Stop();
            ActionHost.OnTargetWindowPicked -= SetTargetWnd;
           
        }

        /// <summary>
        /// Включить поток прослушивания событий 
        /// </summary>
        public void EnableListeningHook()
        {
            ActionHost.OnItemAdded += Add;          
            hcListener.SetListenHook();
         
        }


        /// <summary>
        /// Отключить поток прослушивания событий 
        /// </summary>
        public void DisableListeningHook()
        {
            Stop();
            ActionHost.OnItemAdded -= Add;
            this.TargetWnd = IntPtr.Zero;
        }
        
    }
}
