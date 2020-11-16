using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ALT.Framework.Mvc.Helpers
{
    #region >> Action 결과를 gzip로 압축
    public class CompressAttribute : ActionFilterAttribute
    {
        #region >> 주석
        /*
         *
         * 참고 : https://rueisnom.com/ko/q/6ab28c
         * 다음과 같이 web.config 파일을 통해 압축을 구성 할 수 있습니다.

            <system.webServer>
                <urlCompression doStaticCompression="true" doDynamicCompression="true" />
            </system.webServer>
            iis.net/ConfigReference 에서이 구성 요소의 문서를 찾을 수 있습니다. 이것은 다음과 같습니다.

            IIS (인터넷 정보 서비스) 열기
            수정할 가상 디렉터리에 도달 할 때까지 왼쪽의 트리보기를 탐색합니다.
            오른쪽 창의 제목이 상기 가상 디렉터리의 이름이되도록 적절한 가상 디렉터리를 선택합니다.
            오른쪽 창의 "IIS"아래에서 "압축"선택
            두 옵션을 모두 선택하고 맨 오른쪽의 "작업"에서 "적용"을 선택하십시오.
            참고 : (주석에서 지적한 바와 같이) Http Dynamic Compression이 설치되어 있는지 확인해야합니다. 그렇지 않으면 doDynamicCompression="true" 설정 doDynamicCompression="true" 아무 효과가 없습니다. 가장 빠른 방법은 다음과 같습니다.

            시작> optionalfeatures 입력 ( "Windows 기능 켜기 또는 끄기"창으로 이동하는 가장 빠른 방법입니다)
            "Windows 기능"트 리뷰의 인터넷 정보 서비스> World Wide Web 서비스> 성능 기능으로 이동하십시오.
            "동적 콘텐츠 압축"이 선택되었는지 확인하십시오.
            "확인"을 클릭하고 Windows가 구성 요소를 설치하는 동안 기다립니다.
         */
        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var _encodingsAccepted = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
                if (string.IsNullOrEmpty(_encodingsAccepted)) return;

                _encodingsAccepted = _encodingsAccepted.ToLowerInvariant();
                var _response = filterContext.HttpContext.Response;

                if (_encodingsAccepted.Contains("deflate"))
                {
                    _response.AppendHeader("Content-encoding", "deflate");
                    _response.Filter = new DeflateStream(_response.Filter, CompressionMode.Compress);
                }
                else if (_encodingsAccepted.Contains("gzip"))
                {
                    _response.AppendHeader("Content-encoding", "gzip");
                    _response.Filter = new GZipStream(_response.Filter, CompressionMode.Compress);
                }
            }
            catch (Exception) { }
        }
    }
    #endregion
}
