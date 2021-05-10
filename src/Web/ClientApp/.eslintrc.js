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
        "import/prefer-default-export": ["off"],
        "class-methods-use-this": ["off"],
        "@typescript-eslint/object-curly-spacing": ["warn"],
        "object-shorthand": ["warn"],
        "prefer-destructuring": ["warn", {"object": false, "array": false} ],
        "unicode-bom": ["off"],
        "max-len": ["warn", { "code": 190 }],
        "no-restricted-globals": ["off"],
        "@typescript-eslint/lines-between-class-members": ["off"],
        "no-alert": ["off"],
        "no-console": ["off"],
        "@typescript-eslint/no-unused-vars": ["warn"],
    }
};