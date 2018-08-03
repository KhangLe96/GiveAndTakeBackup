import React from 'react';
import { connect } from 'dva';
import CategoryList from '../CategoryManagement/CategoryList';

@connect(({ modals, management }) => ({
  ...modals, ...management,
}))
export default class index extends React.Component {
  componentDidMount() {
    const { categories, dispatch } = this.props;
    if (categories.length === 0) {
      dispatch({
        type: 'management/fetchCategory',
        payload: {},
      });
    }
  }

  render() {
    const { categories } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý danh mục</h1>
        </div>
        <div className="containerBody">
          <CategoryList
            categories={categories}
            dispatch={this.props.dispatch}
          />
        </div>
      </div>
    );
  }
}
