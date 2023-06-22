FROM node:14

ENV PORT 2567

WORKDIR /usr/src/app

# A wildcard is used to ensure both package.json AND package-lock.json are copied
COPY package*.json ./

RUN npm create colyseus-app@latest
RUN npm ci
# run this for production
# npm ci --only=production

COPY . .

EXPOSE 2567

#RUN npm run start
CMD [ "npm", "start" ]