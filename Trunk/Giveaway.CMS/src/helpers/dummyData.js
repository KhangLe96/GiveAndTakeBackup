import * as uuid from 'uuid';

let dummyArticles = [
  {
    thumbnailUrl: 'https://loremflickr.com/g/320/240/food',
    title: 'Món ăn nổi tiếng tại Hạ Long bạn cần phải thử',
    summary: 'Fusce lacinia et nisi vitae rhoncus. Fusce laoreet scelerisque lorem, rutrum faucibus nunc iaculis id. Pellentesque pellentesque felis quis vulputate ornare. Donec nec ipsum et lacus fermentum iaculis. Sed vestibulum consectetur pretium. Integer quis faucibus nibh, non consectetur augue. Duis congue neque sit amet malesuada commodo. Cras et dolor vitae purus facilisis tempus vel a neque. Curabitur nunc nisi, interdum vitae pellentesque eget, laoreet ut libero. Cras sodales nibh eget velit fermentum gravida. Nam pellentesque justo at lectus gravida accumsan. Sed vitae aliquam ex. Morbi quis quam vel arcu commodo volutpat ac in orci. ',
    slug: 'mon-an-noi-tieng-tai-ha-long-ban-can-phai-thu',
  },
  {
    thumbnailUrl: 'https://loremflickr.com/g/320/240/sea',
    title: 'Muốn đến ngay Phan Thiết sau khi đọc xong cẩm nang du lịch bụi',
    summary: 'Proin luctus nisl at facilisis sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla lorem dolor, lacinia ut purus sit amet, auctor mattis leo. Vivamus id tempor libero. Phasellus eu ligula ut mauris euismod pharetra nec a nisl. Sed a lectus ac lectus egestas sollicitudin. Quisque dictum, neque at varius scelerisque, ligula odio placerat ipsum, sed pretium urna metus a libero. Vivamus nec ultricies erat. Aliquam eu erat tempus nibh congue elementum a at ante. Cras congue libero orci, sed pellentesque justo fermentum non. Etiam interdum lacinia cursus. Mauris id turpis ipsum. Vivamus id fringilla magna. Suspendisse sodales diam vitae orci mattis placerat. ',
    slug: 'muon-den-ngay-phan-thiet-sau-khi-doc-xong-cam-nang-du-lich-bui',
  },
  {
    thumbnailUrl: 'https://loremflickr.com/g/320/240/camping',
    title: 'Cắm trại tại địa điểm cực hot tại Đà Nẵng',
    summary: 'Proin fringilla, risus eu ultrices congue, orci ante sollicitudin lacus, nec posuere turpis quam ultricies quam. Morbi libero sapien, pulvinar sed mi ornare, elementum venenatis velit. Morbi volutpat justo vel dolor venenatis, ac consectetur turpis consequat. Donec sed nulla ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Curabitur tincidunt non mauris non sodales. Ut laoreet rhoncus odio eget rhoncus. ',
    slug: 'mon-an-noi-tieng-tai-ha-long-ban-can-phai-thu',
  },
  {
    thumbnailUrl: 'https://loremflickr.com/g/320/240/trip',
    title: 'Du lịch sa mạc ngay tại Việt Nam, tại sao không?',
    summary: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin non placerat ex. Ut elementum nisi sed nisi interdum lobortis. Nunc varius tortor vel ultricies pharetra. Cras libero nibh, porttitor nec nunc a, cursus condimentum neque. Aenean sagittis convallis euismod. Aliquam euismod hendrerit est, id consequat mi mattis in. Integer neque mi, sodales quis metus et, posuere viverra nibh. Nunc porttitor magna eu tincidunt gravida. Vestibulum placerat feugiat erat, at lobortis odio. Nunc enim lorem, malesuada vel enim a, luctus tincidunt magna. Vivamus id blandit eros, ut mollis quam. ',
    slug: 'du-lich-sa-mac-ngay-tai-viet-nam-tai-sao-khong',
  },
];

let dummyFeaturedTrips = [
  {
    from: 'Huế',
    destination: 'Đà Nẵng',
    date: '12/10/2018',
    time: '08:20',
    model: 'Audi A1 2018',
    seat: '5/8',
    type: 'Cố định',
    fullname: 'Quy Ho',
    ratePoint: 5,
    rateTitle: 'Rất tốt',
    fare: '89 000',
  },
  {
    from: 'Huế',
    destination: 'Đà Nẵng',
    date: '16/10/2018',
    time: '03:50',
    model: 'Audi 01 2018',
    seat: '2/8',
    type: 'Cố định',
    fullname: 'Quy Ho',
    ratePoint: 4.5,
    rateTitle: 'Rất tốt',
    fare: '99 000',
    background_type: 'gray',
    isIntergratedCar: true,
  },
  {
    from: 'Tam Kỳ',
    destination: 'Đà Nẵng',
    date: '10/10/2018',
    time: '19:20',
    model: 'Honda Corolla 2018',
    seat: '3/8',
    type: 'Cố định',
    fullname: 'Quy Ho',
    ratePoint: 2.5,
    rateTitle: 'Rất tốt',
    fare: '120 000',
  },
  {
    from: 'Huế',
    destination: 'Đà Nẵng',
    date: '16/10/2018',
    time: '10:30',
    background_type: 'gray',
    model: 'Audi 01 2018',
    seat: '4/8',
    type: 'Cố định',
    fullname: 'Quy Ho',
    ratePoint: 3,
    rateTitle: 'Rất tốt',
    fare: '60 000',
    isIntergratedCar: true,
  },
  {
    from: 'Quảng Nam',
    destination: 'Đà Nẵng',
    date: '10/05/2018',
    time: '05:20',
    model: 'Audi 01 2018',
    seat: '5/8',
    type: 'Tận nơi',
    fullname: 'Quy Ho',
    ratePoint: 4.5,
    rateTitle: 'Rất tốt',
    fare: '220 000',
  },
];

let dummyNotificationItems = [
  {
    type: 'review',
    meta: {
      from: 'Nhung Lê',
      to: 'London - France',
      rate: 5,
    },
    ctime: '5 ngày trước',
  },
  {
    type: 'trip',
    meta: {
      from: 'Nhung Lê',
      to: 'Đà Nẵng - Hải Phòng',
      seats: 5,
    },
    ctime: '10 phút trước',
  },
  {
    type: 'comment',
    meta: {
      from: 'John Harry',
      to: 'Đà Nẵng - Bình Thuận',
    },
    ctime: '1 ngày trước',
  },
  {
    type: 'trip',
    meta: {
      from: 'Minh Văn',
      to: 'Đà Nẵng - Bình Thuận',
      seats: 5,
    },
    ctime: '3 ngày trước',
  },
  {
    type: 'review',
    meta: {
      from: 'Nhung Lê',
      to: 'Phan Thiết - Quy Nhơn',
      rate: 3.5,
    },
    ctime: '2 giờ trước',
  },
];

dummyArticles = dummyArticles.map((item) => {
  return { ...item, key: uuid.v1() };
});
dummyFeaturedTrips = dummyFeaturedTrips.map((item) => {
  return { ...item, key: uuid.v1() };
});
dummyNotificationItems = dummyNotificationItems.map((item) => {
  return { ...item, key: uuid.v1() };
});
export default {
  dummyArticles,
  dummyFeaturedTrips,
  dummyNotificationItems,
};
