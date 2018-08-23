import * as _ from 'lodash';

const templates = {
  review: '<strong><%= from %></strong> đã đánh giá <strong><%= rate %> sao</strong> cho bạn trong chuyến xe từ <strong><%= to %></strong>',
  trip: '<strong><%= from %></strong> đã đặt <strong><%= seats %> chỗ ngồi</strong> trong chuyến xe từ <strong><%= to %></strong>',
  comment: '<strong><%= from %></strong> đã bình luận trong chuyến xe từ <strong><%= to %></strong>',
  system_activate_profile: '<strong><%= from %></strong> đã thông báo rằng bạn cần phải xác nhận thông tin chung',
};

const NotificationTemplate = {
  make: (type, meta) => {
    const f = _.template(templates[type]);
    return f(meta);
  },
};
export default NotificationTemplate;
