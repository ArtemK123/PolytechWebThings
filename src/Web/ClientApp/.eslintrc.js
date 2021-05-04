module.exports = {
    extends: ["airbnb-typescript/base"],
    parserOptions: {
        project: "./tsconfig.json",
    },
    rules: {
        "linebreak-style": ["warn", "windows"],
        "indent": ["warn", 4],
        "@typescript-eslint/indent": ["warn", 4],
        "quotes": ["warn", "double"],
        "@typescript-eslint/quotes": ["warn", "double"],
        "eol-last": ["warn", "never"],
    }
};