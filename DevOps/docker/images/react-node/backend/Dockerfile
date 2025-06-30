FROM node:20-alpine

# Set work directory
WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm install

# Copy code and build
COPY . .
RUN npm run build

# Expose port
EXPOSE 3002

# Run app
CMD ["npm", "start"]