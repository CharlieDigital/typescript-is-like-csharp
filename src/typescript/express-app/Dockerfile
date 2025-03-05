# Use alpine for size, but feel free to use other builds if running into issues.
FROM node:20-alpine

# Create app directory in the image
WORKDIR /usr/src/app

# Copy over assets
COPY package.json ./
COPY package-lock.json ./

# Install dependencies.
RUN npm ci

# Copy source
COPY . .

# Build the TypeScript
RUN npx tsc

# Start the server.
EXPOSE 3001
CMD ["node", "dist/index.js"]
