import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm, Input } from 'antd';
import { Link } from 'dva/router';
import moment from 'moment';

import { DateFormatDisplay, TABLE_PAGESIZE, ENG_VN_DICTIONARY, COLOR, STATUSES } from '../../../common/constants';

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
        dataIndex: 'statusCMS',
        key: 'statusCMS',
        render: val => ENG_VN_DICTIONARY[val],
      }, {
        title: 'Hành động',
        key: 'Action',
        render: (record) => {
          let buttonContent = 'Khóa';
          let buttonIcon = 'lock';
          let buttonType = 'danger';
          let newPostStatus = STATUSES.Blocked;
          let popConfirmTitle = 'Bạn chắc chắn muốn khóa bài đăng này?';
          if (record.statusCMS === STATUSES.Blocked) {
            buttonContent = 'Mở khóa';
            buttonIcon = 'unlock';
            buttonType = 'default';
            newPostStatus = STATUSES.Activated;
            popConfirmTitle = 'Bạn có muốn mở lại bài đăng này?';
          }
          return (
            <span>
              <Popconfirm
                title={popConfirmTitle}
                onConfirm={() => {
                  const { dispatch, posts } = this.props;
                  dispatch({
                    type: 'postManagement/changeCMSStatus',
                    payload: { id: record.id, statusCMS: newPostStatus, posts },
                  });
                }}
              >
                <Button type={buttonType} icon={buttonIcon}>{buttonContent}</Button>
              </Popconfirm>
              <Divider type="vertical" />
              <Popconfirm
                title="Cảnh cáo người dùng này?"
              >
                <Button type="primary" icon="warning" style={{ background: COLOR.Warning }}>Cảnh báo người dùng</Button>
              </Popconfirm>
            </span >
          );
        },
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
