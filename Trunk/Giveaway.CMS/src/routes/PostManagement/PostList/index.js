import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm, Input } from 'antd';
import { Link } from 'dva/router';
import moment from 'moment';

import { DateFormatDisplay, TABLE_PAGESIZE, POST_STATUSES, WARNING_COLOR } from '../../../common/constants';

export default class index extends React.Component {

  columns =
    [
      {
        title: 'Tiêu đề',
        key: 'title',
        render: record => <Link to={`/post-management/detail/${record.id}`}>{record.title}</Link>,
      },
      {
        title: 'Người đăng',
        key: 'user',
        render: record => <Link to={`/user-management/detail/${record.userId}`}>{record.userId}</Link>,
      },
      {
        title: 'Địa chỉ',
        dataIndex: 'address.provinceCityName',
        key: 'address.provinceCityName',
      },
      {
        title: 'Category',
        dataIndex: 'category',
        key: 'category.categoryName',
        render: category => <Link to={`/category-management/detail/${category.id}`}>{category.categoryName}</Link>,

      },
      {
        title: 'Ngày đăng',
        dataIndex: 'createdTime',
        key: 'dayPost',
        render: val => <span>{moment.utc(val).local().format(DateFormatDisplay)}</span>,
      },
      {
        title: 'Trạng thái',
        dataIndex: 'status',
        key: 'status',
        render: val => POST_STATUSES[val],
      }, {
        title: 'Hành động',
        dataIndex: 'postId',
        key: 'Action',
        render: id => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn muốn ẩn bài đăng này?"
              onConfirm={() => {
                const { dispatch, posts } = this.props;
                dispatch({
                  type: 'postManagement/deletePost',
                  payload: { posts, id },
                });
              }}
            >
              <Button type="danger" icon="delete">Ẩn bài đăng</Button>
            </Popconfirm>
            <Divider type="vertical" />
            <Popconfirm
              title="Cảnh cáo người dùng này?"
            >
              <Button type="primary" icon="warning" style={{ background: WARNING_COLOR }}>Cảnh báo người dùng</Button>
            </Popconfirm>
          </span >),
      },
    ];

  render() {
    const { posts } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={posts.map((post, key) => { return { ...post, key }; })}
        pagination={{ pageSize: TABLE_PAGESIZE }}
      />
    );
  }
}
