using System;
using System.Collections.Generic;
using System.Text;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// my singelton pattarn for getting a socket class, just to make sure i only have 1 instance of the socket class.
    /// </summary>
    public static class SocketCreator
    {
        private static SocketHelper instance;
        
        public static SocketHelper getInstance()
        {
            if (instance == null)
            {
                instance = new SocketHelper();
            }
            return instance;
        }

    }
}
