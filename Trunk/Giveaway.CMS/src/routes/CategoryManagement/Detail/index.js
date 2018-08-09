import React, { PureComponent } from 'react';
import { Button, Divider, Form, Input, Popconfirm } from 'antd';
import { connect } from 'dva';
import { routerRedux } from 'dva/router';
import UploadImage from '../Create/UploadImage';

import { ENG_VN_DICTIONARY, STATUSES } from '../../../common/constants';

import './index.less';

const FormItem = Form.Item;
const DEFAULT_NO_IMAGE_PATH = './images/noImage.jpg';

@connect(({ categoryManagement, modals }) => ({
  ...categoryManagement, ...modals,
}))
@Form.create()
export default class index extends PureComponent {
  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount() {
    const { dispatch, match: { params } } = this.props;
    dispatch({
      type: 'categoryManagement/getACategory',
      payload: {
        id: params.id,
      },
    });
  }

  changeCMSStatus = () => {
    const { dispatch, currentPage, selectedCategory: { id, status } } = this.props;
    let buttonContent = 'Khóa';
    let buttonIcon = 'lock';
    let buttonType = 'danger';
    let newCMSStatus = STATUSES.Blocked;
    let popConfirmTitle = 'Bạn chắc chắn muốn khóa danh mục này?';
    if (status === STATUSES.Blocked) {
      buttonContent = 'Mở khóa';
      buttonIcon = 'unlock';
      buttonType = 'primary';
      newCMSStatus = STATUSES.Activated;
      popConfirmTitle = 'Bạn có muốn mở lại danh mục này?';
    }
    return (
      <Popconfirm
        title={popConfirmTitle}
        onConfirm={() => {
          dispatch({
            type: 'categoryManagement/changeCMSStatus',
            payload: { CMSStatus: newCMSStatus, id, page: currentPage },
          });
        }}
      >
        <Button type={buttonType} icon={buttonIcon}>{buttonContent}</Button>
      </Popconfirm>
    );
  }

  handleSubmit = () => {
    const { dispatch, form: { validateFields }, match: { params } } = this.props;
    validateFields((err, value) => {
      if (!err) {
        const payload = {
          category: {
            categoryName: value.categoryName,
            categoryImageUrl: (value.categoryImageUrl) ? value.categoryImageUrl : DEFAULT_NO_IMAGE_PATH,
          },
          id: params.id,
        };
        dispatch({
          type: 'categoryManagement/update',
          payload,
        });
      }
    });
  }

  handleDelete = () => {
    const { dispatch, totals, selectedCategory: { id } } = this.props;
    dispatch({
      type: 'categoryManagement/delete',
      payload: { id, totals },
    });
    dispatch(routerRedux.push('/category-management'));
  }

  render() {
    const { form: { getFieldDecorator }, selectedCategory: { categoryName, status } } = this.props;
    return (
      <div className="containerBody">
        <Form onSubmit={this.handleSubmit}>
          <FormItem
            label="Tên danh mục"
          >
            {getFieldDecorator('categoryName', {
              rules: [{
                required: true, message: 'Vui lòng nhập tên danh mục!',
              }],
              initialValue: categoryName,
            })(
              <Input />
            )}
          </FormItem>
          <FormItem
            label="Tình trạng"
          >
            {getFieldDecorator('status', {
              rules: [{
                required: true,
              }],
              initialValue: ENG_VN_DICTIONARY[status],
            })(
              <Input disabled />
            )}
          </FormItem>
          <FormItem
            label="Hình ảnh"
          >
            {getFieldDecorator('image')(<UploadImage />)}
          </FormItem>
          <Button type="primary" htmlType="submit" className="login-form-button">Cập nhập</Button>
          <Divider type="vertical" />
          {this.changeCMSStatus()}
          <Divider type="vertical" />
          <Popconfirm
            title="Bạn chắc chắn muốn xóa?"
            onConfirm={this.handleDelete}
          >
            <Button icon="delete" type="danger">Xóa</Button>
          </Popconfirm>
        </Form>
      </div>
    );
  }
}
