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
    extend: {
      colors: {
        primary: {
          50: '#eff6ff',
          100: '#dbeafe',
          200: '#bfdbfe',
          300: '#93c5fd',
          400: '#60a5fa',
          500: '#3b82f6',
          600: '#2563eb',
          700: '#1d4ed8',
          800: '#1e40af',
          900: '#1e3a8a',
        },
        secondary: {
          50: '#f8fafc',
          100: '#f1f5f9',
          200: '#e2e8f0',
          300: '#cbd5e1',
          400: '#94a3b8',
          500: '#64748b',
          600: '#475569',
          700: '#334155',
          800: '#1e293b',
          900: '#0f172a',
        },
      },
    },
  },
  plugins: [],
}

