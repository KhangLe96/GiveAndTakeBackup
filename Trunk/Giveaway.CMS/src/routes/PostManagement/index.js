import { Input, Select, Button, Form } from 'antd';
import React from 'react';
import { connect } from 'dva';
import PostList from './PostList';

const FormItem = Form.Item;
const { Option } = Select;

@connect(({ modals, postManagement }) => ({
  ...modals, ...postManagement,
}))
class AdvancedSearchForm extends React.Component {

  componentDidMount() {
    const { dispatch } = this.props;
    dispatch({
      type: 'postManagement/fetch',
      payload: {},
    });
  }

  handleSubmit = (e) => {
    e.preventDefault();
    this.props.form.validateFieldsAndScroll((err, values) => {
      if (!err) {
        if (values.typeSelect === 'categoryName') {
          this.state.requestValue = {
            categoryName: values.searchValue,
          };
        }
        if (values.typeSelect === 'postStatus') {
          this.state.requestValue = {
            postStatus: values.searchValue,
          };
        }
        if (values.typeSelect === 'address') {
          this.state.requestValue = {
            address: values.searchValue,
          };
        }
        const c = this.state.requestValue;
        const { dispatch } = this.props;
        if (!err) {
          dispatch({
            type: 'postManagement/findPost',
            payload: { ...c },
          });
        }
      }
    });
  }

  render() {
    const { form: { getFieldDecorator }, posts } = this.props;
    const searchFilterWidth = 200;
    const searchInputWidth = 500;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý bài đăng</h1>
        </div>
        <div className="containerBody">
          <Form layout="inline" onSubmit={this.handleSubmit} className="AdvancedSearchForm">
            <FormItem>
              {getFieldDecorator('typeSelect')(
                <Select
                  style={{ width: searchFilterWidth }}
                  placeholder="Select type to filter"
                  optionFilterProp="children"
                  size="large"
                  filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
                >
                  <Option value="postStatus">Status</Option>
                  <Option value="categoryName">Category Name</Option>
                  <Option value="address">Address</Option>
                </Select>
              )}
            </FormItem>
            <FormItem>
              {getFieldDecorator('searchValue')(
                <Input size="large" style={{ width: searchInputWidth }} />
              )}
            </FormItem>
            <FormItem>
              <Button type="primary" htmlType="submit" >
                Search
              </Button>
            </FormItem>
          </Form>
        </div>
        <div className="containerBody">
          <PostList
            posts={posts}
            dispatch={this.props.dispatch}
          />
        </div>
      </div>
    );
  }
}
export default Form.create()(AdvancedSearchForm);
