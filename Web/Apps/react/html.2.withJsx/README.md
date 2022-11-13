# Steps

1. **Setup codebase**
```
index.html
src/like_button.js (write here the js code)
```

2. **Prepare nodejs** ([source](https://reactjs.org/docs/add-react-to-a-website.html#add-jsx-to-a-project))

    1. Run `npm init -y`
    2. Run `npm install babel-cli@6 babel-preset-react-app@3`
    3. Run `npx babel --watch src --out-dir . --presets react-app/prod`

3. Minify our code
    1. run `npx terser -c -m -o like_button.min.js -- like_button.js`

4. **Load the right files**
```
index.html
like_button.min.js
react.production files
```
5. **Delete unnecessary files**
```

```
