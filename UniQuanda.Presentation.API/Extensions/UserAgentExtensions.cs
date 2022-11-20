using UniQuanda.Core.Domain.Utils;
using UAParser;

namespace UniQuanda.Presentation.API.Extensions
{
		public static class UserAgentInfoExtensions
		{
				public static UserAgentInfo GetUserAgentInfo(this HttpContext context)
				{
						string? userAgentString = context.Request.Headers["User-Agent"];
						if (string.IsNullOrEmpty(userAgentString)) {
								return new UserAgentInfo { Browser = null, Os = null };
						}

						var uaParser = Parser.GetDefault();
						ClientInfo clientInfo = uaParser.Parse(userAgentString);

						return new UserAgentInfo { 
								Browser = clientInfo.UA.ToString(), 
								Os = clientInfo.Device.ToString() 
						};
				}
		}
}