using ALT.BizService;
using loggalServiceBiz;

namespace loggalWebMng.CommonCS
{
    public static class Service
    {
        private static ALT.BizService.EmployeeService _employeeService;
        public static ALT.BizService.EmployeeService employeeService
        {
            get
            {
                if (_employeeService == null) _employeeService = new ALT.BizService.EmployeeService();
                return _employeeService;
            }
        }

        private static CommonService _commonService;
        public static CommonService commoneService
        {
            get
            {
                if (_commonService == null) _commonService = new CommonService();
                return _commonService;
            }
        }

        private static DeviceService _deviceService;
        public static DeviceService deviceService
        {
            get
            {
                if (_deviceService == null) _deviceService = new DeviceService();
                return _deviceService;
            }
        }
        private static BasicService _basicService;
        public static BasicService basicService
        {
            get
            {
                if (_basicService == null) _basicService = new BasicService();
                return _basicService;
            }
        }
   }
}