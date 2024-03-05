FROM nginx:alpine

RUN chmod g+rwx /var/cache/nginx /var/run /var/log/nginx
RUN chmod -R 775 /etc/nginx
RUN chmod -R 775 /etc/nginx/conf.d
RUN chmod -R 775 /etc/nginx/conf.d/default.conf
RUN chmod -R 775 /var/cache/nginx
RUN mkdir /var/cache/nginx/client_temp
RUN mkdir /var/cache/nginx/proxy_temp
RUN mkdir /var/cache/nginx/fastcgi_temp
RUN mkdir /var/cache/nginx/uwsgi_temp
RUN mkdir /var/cache/nginx/scgi_temp

RUN /bin/ash -c 'chmod g+rwx /var/cache/nginx /var/run /var/log/nginx &&\
    chmod 0775 /var/cache/nginx &&\
    chmod g+rwx /etc/nginx &&\
    chmod g+rwx /etc/nginx/conf.d &&\
    chmod g+rwx /etc/nginx/conf.d/default.conf &&\
    chmod g+rwx /etc/nginx/nginx.conf &&\
    chmod g+rwx /var/cache/nginx/client_temp &&\
    chmod g+rwx /var/cache/nginx/proxy_temp &&\
    chmod g+rwx /var/cache/nginx/fastcgi_temp &&\
    chmod g+rwx /var/cache/nginx/uwsgi_temp &&\
    chmod g+rwx /var/cache/nginx/scgi_temp'



WORKDIR /etc/nginx/conf.d
COPY webgl.conf default.conf

WORKDIR /webgl/poidotoho
COPY ./builds/webgl/ .