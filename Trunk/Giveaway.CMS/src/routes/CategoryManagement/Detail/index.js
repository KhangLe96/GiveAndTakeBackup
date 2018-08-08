import React, { PureComponent } from 'react';
import { Button, Form, Input, Popconfirm } from 'antd';
import { connect } from 'dva';
import { routerRedux } from 'dva/router';
import UploadImage from '../Create/UploadImage';

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

  render() {
    const { form: { getFieldDecorator }, selectedCategory: { categoryName, categoryImageUrl, id } } = this.props;
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
            label="Hình ảnh"
          >
            {getFieldDecorator('image')(<UploadImage />)}
          </FormItem>
          <Button type="primary" size="large" htmlType="submit" className="login-form-button">Cập nhập</Button>
          <Popconfirm
            title="Bạn chắc chắn muốn xóa?"
            onConfirm={() => {
              const { dispatch, totals } = this.props;
              dispatch({
                type: 'categoryManagement/delete',
                payload: { id, totals },
              });
              dispatch(routerRedux.push('/category-management'));
            }}
          >
            <Button type="danger" size="large" className="login-form-button">Xóa</Button>
          </Popconfirm>
        </Form>
      </div>
    );
  }
}
