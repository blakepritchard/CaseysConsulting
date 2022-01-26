
const connectionString = process.env.message;
module.exports = async function (context, req) {
    var strMessage = "Howdy"; // process.env.message;

    context.res.json({
        text: connectionString
    });
};