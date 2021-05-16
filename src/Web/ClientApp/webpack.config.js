const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const ESLintPlugin = require("eslint-webpack-plugin");

const isDevelopment = process.env.NODE_ENV !== "production";
// eslint-disable-next-line no-console
console.log(`Using NODE_ENV = ${process.env.NODE_ENV}`);

module.exports = {
    mode: isDevelopment ? "development" : "production",
    entry: "./src/index.ts",
    module: {
        rules: [
            {
                test: /\.ts?$/,
                use: "ts-loader",
                exclude: /node_modules/,
            },
            {
                test: /\.html$/,
                use: ["html-loader"],
            },
            {
                test: /\.s[ac]ss$/i,
                use: [
                    // Creates `style` nodes from JS strings
                    "style-loader",
                    // Translates CSS into CommonJS
                    "css-loader",
                    // Compiles Sass to CSS
                    "sass-loader",
                ],
            },
        ],
    },
    resolve: {
        extensions: [".ts", ".js", ".scss", ".html"],
        alias: {
            src: path.resolve("./src"),
        },
    },
    plugins: [
        new HtmlWebpackPlugin({
            title: "Polytech WebThings",
            favicon: "./src/assets/favicon.ico",
        }),
        new ESLintPlugin({
            extensions: ["ts"],
        }),
    ],
    output: {
        filename: "[name].[contenthash].js",
        path: path.resolve(__dirname, "dist"),
        clean: true,
        publicPath: "/",
    },
    optimization: {
        moduleIds: "deterministic",
        runtimeChunk: "single",
        splitChunks: {
            cacheGroups: {
                vendor: {
                    test: /[\\/]node_modules[\\/]/,
                    name: "vendors",
                    chunks: "all",
                },
            },
        },
    },
};