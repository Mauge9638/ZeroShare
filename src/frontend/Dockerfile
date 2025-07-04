FROM node:22-alpine AS build
WORKDIR /app

ARG API_BASE_URL
ARG INACTIVITY_RETENTION_DAYS
ENV VITE_API_BASE_URL=$API_BASE_URL
ENV VITE_INACTIVITY_RETENTION_DAYS=$INACTIVITY_RETENTION_DAYS

RUN npm install -g pnpm
COPY package.json pnpm-lock.yaml ./
RUN pnpm install --frozen-lockfile

COPY . .
RUN pnpm build

# Production stage with nginx
FROM nginx:alpine AS production
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80 443
CMD ["nginx", "-g", "daemon off;"]