using System;

namespace Jyi.Entity
{
    class ProxyInfo
    {
        private string m_Name;
        private string m_Address;
        private int m_Port;
        private bool m_Checking = false;

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }
        public int Port
        {
            get { return m_Port; }
            set { m_Port = value; }
        }
        public bool HaveGet
        {
            get { return m_Checking; }
            set { m_Checking = value; }
        }
    }
}
