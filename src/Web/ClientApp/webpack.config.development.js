const { merge } = require("webpack-merge");
const path = require("path");
const commonConfig = require("./webpack.config.js");

const webpackDevServerPort = 7406;
const backendUrl = "http://localhost:7400";

const devConfig = {
    devtool: "inline-source-map",
    devServer: {
        contentBase: path.join(__dirname, "dist"),
        compress: true,
        port: webpackDevServerPort,
        proxy: {
            "/api": backendUrl,
        },
        historyApiFallback: {
            historyApiFallback: {
                index: "index.html",
            },
        },
    },
};

module.exports = merge(commonConfig, devConfig);