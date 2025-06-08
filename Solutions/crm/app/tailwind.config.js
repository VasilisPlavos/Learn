/** @type {import('tailwindcss').Config} */
module.exports = {
  // NativeWind: Add the paths to all of the component files
  // https://www.nativewind.dev/docs/getting-started/installation#2-setup-tailwind-css
  content: [
    './app/**/*.{js,ts,tsx}',
    './assets/**/*.{js,ts,tsx}',
    './components/**/*.{js,ts,tsx}',
    './constants/**/*.{js,ts,tsx}',
    './hooks/**/*.{js,ts,tsx}'
  ],
  presets: [require("nativewind/preset")],
  theme: {
    extend: {},
  },
  plugins: [],
}

