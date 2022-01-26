module.exports = async function (context, req) {
    var strMessage = process.env.message;
    context.res.json({
        text: "Hello World"
    });
};