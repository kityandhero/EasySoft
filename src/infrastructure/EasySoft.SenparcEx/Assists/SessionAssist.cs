using Senparc.Weixin.WxOpen.Containers;

namespace EasySoft.SenparcEx.Assists
{
    public static class SessionAssist
    {
        public static SessionBag GetSession(string sessionId)
        {
            var registered = SessionContainer.CheckRegistered(sessionId);

            if (!registered)
            {
                throw new Exception("sessionId is not registered");
            }

            var sessionBag = SessionContainer.GetSession(sessionId);

            if (sessionBag == null)
            {
                throw new Exception("sessionBag is null");
            }

            return sessionBag;
        }
    }
}