
const constSecret = process.env.BotSecret;
module.exports = async function (context, req) {
    var iframeChat = "<iframe src='https://webchat.botframework.com/embed/CaseysConsultingBot?s=" + constSecret +"'  style='min-width: 400px; width: 100%; min-height: 500px;'></iframe>"; // process.env.message;

    context.res.json({
        text: iframeChat 
    });
};